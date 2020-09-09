using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;
using HW3.Common.DTO;
using HW3.DAL.Models;
using System.Threading;
using System.Diagnostics;

namespace HW3.Client

{
    internal static class Program
    {
        public static HttpService httpService=new HttpService();
        public static TimerWrapper queries = new TimerWrapper();
        private static async Task Main()
        {
            bool flag = true;
            int answ;
            while (flag)
            {
                await ChooseAction().ConfigureAwait(false);
                Console.WriteLine("Чи хочеш ти виконати ще якусь дiю? 1-так/2-нi");
                answ = int.Parse(Console.ReadLine());
                if (answ != 1)
                    flag = false;
            }
         }
        public static async Task ChooseAction()
        {
            int action;
            Console.WriteLine("Обери, що ти хочеш зробити");
            Console.WriteLine("1-Отримати кількість тасків у проекті конкретного користувача (по id)" +
                "\n2-Отримати список тасків, призначених для конкретного користувача (по id), де name таска <45 символів" +
                " \n3-Отримати список (id, name) з колекції тасків, які виконані (finished) в поточному (2020) році для конкретного користувача (по id).\n" +
             "\n4-Отримати список (id, ім'я команди і список користувачів) з колекції команд, учасники яких старші 10 років, відсортованих за датою реєстрації користувача за спаданням, а також згрупованих по командах." +
             "\n5-Отримати список користувачів за алфавітом first_name (по зростанню) з відсортованими tasks по довжині name (за спаданням)." +
             "\n6-Отримати інформацію про останній проєкт користувача за його id" +
             "\n7-Отримати id проєкту, найдовший та найкоротший таск, в якому загальна кількість символів опису більше 20, а кількість тасків менше 3\n8-Відмітити рандомний таск як завершений");
            Console.WriteLine("Введи свою відповiдь: ");
            bool flag;
            try
            {
                action = int.Parse(Console.ReadLine());
                flag = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                action = int.Parse(Console.ReadLine()); flag = true;
            }

            while (flag)
            {
                int id;
                switch (action)
                {
                    case 1:
                        id = InputId();
                        var one = await httpService.GetEntities<KeyValuePair<int, int>>($"projects/counttasks/{id}").ConfigureAwait(false);
                        foreach (var item in one)
                        {
                            Console.WriteLine("Id проєкту-" + item.Key + "; Кількість тасків-" + item.Value);
                        }
                        flag = false;
                        break;
                    case 2:
                        id = InputId();
                        var two = await httpService.GetEntities<TaskDTO>($"tasks/forUser/{id}").ConfigureAwait(false);
                        Console.WriteLine($"Список тасків користувача {id}");
                        foreach (var item in two)
                        {
                            Console.WriteLine(+item.Id);
                        }
                        flag = false;
                        break;
                    case 3:
                        id = InputId();
                        var three = await httpService.GetEntities<KeyValuePair<int, string>>($"tasks/finished2020/{id}").ConfigureAwait(false);
                        foreach (var item in three)
                        {
                            Console.WriteLine("Id таски - " + item.Key + "; Ім'я таски - " + item.Value);
                        }
                        flag = false;
                        break;
                    case 4:
                        var four = await httpService.GetEntities<TeamPlayersDTO>("teams/users").ConfigureAwait(false);
                        foreach (var item in four)
                        {
                            for (int i = 0; i < item.ListUser.Count; i++)
                                Console.WriteLine("ID команди - " + item.Id + "; Ім'я команди - " + item.Name + "; ID користувача - " + item.ListUser[i].Id);
                        }
                        flag = false;
                        break;
                    case 5:
                        var five = await httpService.GetEntities<UserDTO>("users/tasks").ConfigureAwait(false);
                        foreach (var item in five)
                        {
                            Console.WriteLine("Таски користувача - " + item.FirstName);
                            if (item.Tasks?.Count() != 0)
                            {
                                for (int i = 0; i < item.Tasks.Count; i++)
                                    Console.WriteLine("Id таски - " + item.Tasks[i].Id + "; Ім'я таски - " + item.Tasks[i].Name);
                            }
                        }
                        flag = false;
                        break;
                    case 6:
                        id = InputId();
                        var six = await httpService.GetEntity<AboutLastProjectDTO>($"users/{id}/lastProject").ConfigureAwait(false);
                        Console.WriteLine("Id найдовшого таска - " + six.LongestTask?.Id +
                            "; Кількість незавершених або скасованих тасків користувача - " + six.CountNotFinishedOrCanceledTasks +
                            "; Загальна кількість тасків під останнім проєктом - " + six.CountTasks +
                            "; ID останнього проєкту - " + six.LastProject?.Id);
                        flag = false;
                        break;
                    case 7:
                        var seven = await httpService.GetEntities<AboutProjectDTO>("projects/tasks").ConfigureAwait(false);
                        foreach (var item in seven)
                        {
                            Console.WriteLine("Id проєкту - " + item.Project.Id + "; Id найдовшого проєкту за описом - "
                                + item.TheLongestTask?.Id + "; Id найкоротшого таску по імені - "
                                + item.TheShortestTask?.Id + "; Загальна кількість користувачів в команді - "
                                + item.CountPlayers);
                        }flag = false;
                        break;
                    case 8:
                        WrapperAsync(1000);
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Введи один з варіантів");
                        break;
                }
            }
        }
        public static async void WrapperAsync(int delay)
        {
            try
            {
                var markedTaskId = await queries.MarkRandomTaskWithDelay(delay).ConfigureAwait(false);
                Console.WriteLine(markedTaskId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int InputId()
        {
            Console.WriteLine("Input id");
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch { return -1; }
        }
    }
}

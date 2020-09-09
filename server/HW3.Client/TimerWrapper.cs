using HW3.Common.DTO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace HW3.Client
{
    internal class TimerWrapper
    {
        private readonly HttpService service;
        public TimerWrapper()
        {
            service = new HttpService();
        }
        public Task<int?> MarkRandomTaskWithDelay(int delay)
        {
            TaskCompletionSource<int?> tcs = new TaskCompletionSource<int?>();
            Timer timer = new Timer(delay);
            async void eventHandler(object o, ElapsedEventArgs args)
            {
                timer.Elapsed -= eventHandler;
                var tasks = await service.GetEntities<TaskDTO>("tasks").ConfigureAwait(false);
                Random rdm = new Random();
                var task = tasks[rdm.Next(0, tasks.Count)];
                if (task.State == 2)
                {
                    tcs.SetException(new Exception($"This task {task.Id} was marked as finished earlier"));
                }
                else
                {
                    task.State = 2;
                    await service.Put("tasks", task).ConfigureAwait(false);
                    tcs.SetResult(task.Id);
                }
            }

            timer.Start();
            timer.Elapsed += eventHandler;
            return tcs.Task;
        }
    }
}

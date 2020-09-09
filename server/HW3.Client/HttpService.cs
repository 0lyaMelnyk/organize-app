using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HW3.Client
{
    public class HttpService
    {
        private static readonly HttpClient _client;
        private const string APP_PATH = "https://localhost:44386/api/";

        static HttpService()
        {
            _client = new HttpClient();
        }

        public  async Task<T> GetEntity<T>(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(APP_PATH + path).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            T entity = JsonConvert.DeserializeObject<T>(responseBody);
            return entity;
        }
        public  async Task<string> Post<T>(string token, T obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(APP_PATH + token, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseBody;
        }
        public async Task<List<T>> GetEntities<T>(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(APP_PATH + path).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            List<T> entity = JsonConvert.DeserializeObject<List<T>>(responseBody);
            return entity;
        }
        public async Task<bool> Put<T>(string path,T entity)
        {
            string json = JsonConvert.SerializeObject(entity);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(APP_PATH + path, data).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteEntity(string path, int id)
        {
            var response = await _client.DeleteAsync($"{APP_PATH}{path}/{id}").ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BeerConsupion2.client
{
    public class ApiClient
    {
        static readonly HttpClient client = new HttpClient();
        static ApiClient()
        {
            client.BaseAddress = new Uri("http://localhost:8005/api/");
           
        }

        public static async Task<List<T>> GetAllAsync<T>(string uri)
        {

            string response = await client.GetStringAsync(uri);////////////////////////////////////kodel cia "uri"? ar jis automatiskai supranta kad mes cia kaip prierasa prie linko darom po slasho
            List<T> list = JsonConvert.DeserializeObject<List<T>>(response);
            return list;
        }

        public static async Task PostObjectAsync<T>(T obj, string uri)
        {
            var content = SerializeToContent<T>(obj);
            await client.PostAsync(uri, content);
        }

       
        public static async Task<T> GetObjectAsync<T>(string id, string uri)
        {

            string response = await client.GetStringAsync($"{uri}{id}");///////////////////////////////ka daro tas doleriaus zenklas ir ar jis supranta kad cia tas id yra prierasas prie url?
            T obj = JsonConvert.DeserializeObject<T>(response);
            return obj;
        }

        ///---------------------------------------------------------------------------------------------------
        public static async Task PutObjectAsync<T>(T obj, string id)
        {
            var content = SerializeToContent<T>(obj);
            await client.PutAsync($"{id}", content);
        }


        private static StringContent SerializeToContent<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        public static async Task DeleteByIdAsync(string id,string apiExtention)
        {
            await client.DeleteAsync($"{apiExtention}{id}");
        }



    }
}
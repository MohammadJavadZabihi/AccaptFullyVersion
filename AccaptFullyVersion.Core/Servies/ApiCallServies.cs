using AccaptFullyVersion.Core.Servies.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AccaptFullyVersion.Core.Servies
{
    public class ApiCallServies : IApiCallServies
    {
        public async Task<HttpResponseMessage> SendDeletRequest(string url, object data)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url),
                    Content = content
                };

                var response = await client.SendAsync(request);
                return response;
            }
        }

        public async Task<HttpResponseMessage> SendGetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                var respone = await client.GetAsync(url);
                return respone;
            }
        }

        public async Task<HttpResponseMessage> SendPatchRequest(string url, object data)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PatchAsync(url, content);
                return response;
            }
        }

        public async Task<HttpResponseMessage> SendPostReauest(string url, object data)
        {
            using(var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PostAsync(url, content);
            }
        }

        public async Task<HttpResponseMessage> SendPutRequest(string url, object data)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PutAsync(url, content);
            }
        }
    }
}

using AccaptFullyVersion.Core.Servies.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies
{
    public class ApiCallServies : IApiCallServies
    {
        public async Task<string> SendPostReauest(string url, object data)
        {
            using(var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if(response.IsSuccessStatusCode)
                {
                    var responstcontent = await response.Content.ReadAsStringAsync();
                    dynamic? jsonRespons = JsonConvert.DeserializeObject(responstcontent);

                    string? errorStatuce = jsonRespons;
                    if (!string.IsNullOrWhiteSpace(errorStatuce))
                        return "Messgae : " + errorStatuce;
                    else
                        return $"Error in sending data fpr api with {url}";
                }
                else
                {
                    return $"Error in sending data fpr api with {url} with options";
                }
            }
        }
    }
}

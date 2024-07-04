﻿using AccaptFullyVersion.Core.Servies.Interface;
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
        public async Task<HttpResponseMessage> SendPostReauest(string url, object data)
        {
            using(var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PostAsync(url, content);
            }
        }
    }
}

using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccaptFullyVersion.Web.Controllers
{
    public class ProdcutController1 : Controller
    {
        private readonly IApiCallServies _apiCallServies;
        public ProdcutController1(IApiCallServies apiCallServies)
        {
            _apiCallServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }

        [Route("{ProproductNameduct}")]
        public async Task<IActionResult> GetAllProduct(string ProproductNameduct)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GSP(V1)", ProproductNameduct);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                var product = JsonConvert.DeserializeObject<Product>(response);

                return View(product);
            }

            return NotFound();
        }

        //[HttpPost("Products/{ProproductNameduct}")]
        //public async Task<IActionResult> GetAllProduct(string ProproductNameduct)
        //{
        //    if(!ModelState.IsValid)
        //        return View(ModelState);

        //    var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GSP(V1)", ProproductNameduct);
        //    if(responseMessage.IsSuccessStatusCode)
        //    {
        //        var response = await responseMessage.Content.ReadAsStringAsync();

        //        var product = JsonConvert.DeserializeObject<Product>(response);

        //        return View(product);
        //    }

        //    return NotFound();
        //}
    }
}

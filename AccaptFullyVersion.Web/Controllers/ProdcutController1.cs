using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
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

        [Route("Products")]
        public async Task<IActionResult> GetAllProduct(int pageId = 1, string filter = "")
        {
            var responseMessage = await _apiCallServies.SendGetRequest("https://localhost:7205/api/UserAccount(V1)/GALP(V1)/pageId/filter");

            if(responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                //var product = JsonConvert.DeserializeObject<ProductForShowViewModel>(content);

                return View();
            }

            return NotFound();
        }
    }
}

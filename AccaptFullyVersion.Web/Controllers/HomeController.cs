using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccaptFullyVersion.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiCallServies _apiCallServies;
        public HomeController(IApiCallServies apiCallServies)
        {
            _apiCallServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }
        public async Task<IActionResult> Index(int pahId = 1, int Take = 3, string filter = "string")
        {
            var responseMessage = await _apiCallServies.SendGetRequest($"https://localhost:7205/api/UserAccount(V1)/GALP(V1)/{pahId}/{Take}/{filter}");

            if(responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ShowProductListItemViewModel>>(response);

                return View(products);
            }

            return NotFound();
        }
    }
}
 
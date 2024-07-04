using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccaptFullyVersion.Web.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IApiCallServies _apiCallServies;
        public UserAccountController(IApiCallServies apiCallServies)
        {
            _apiCallServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }

        #region Regsiter User in Web Site

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {
            if(!ModelState.IsValid)
                return View(user);

            var data = new
            {
                Username = user.UserName,
                Email = user.Email,
                Password = user.Password,
                RePassword = user.RePassword
            };


            var response = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/RUA(V1)", data);

            if (response.IsSuccessStatusCode)
                return View("SuccessfullyRegister", user);
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    ModelState.AddModelError(string.Empty, $"{responseContent}");
                    return View(user);
                }

                ModelState.AddModelError(string.Empty, "An error occurred while registering. Please try again later.");
                return View(user);
            }
        }

        #endregion
    }
}

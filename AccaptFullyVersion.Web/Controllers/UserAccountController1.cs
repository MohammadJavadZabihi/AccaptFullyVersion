using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.Web.Controllers
{
    public class UserAccountController1 : Controller
    {
        private readonly IApiCallServies _apiCallServies;
        public UserAccountController1(IApiCallServies apiCallServies)
        {
            _apiCallServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterViewModel user)
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

            var respone = _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/RUA(V1)", data);

            return View("Success");
        }
    }
}

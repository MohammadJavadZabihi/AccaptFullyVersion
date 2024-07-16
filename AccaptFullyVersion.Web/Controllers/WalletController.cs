using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccaptFullyVersion.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IApiCallServies _apiCallServies;
        public WalletController(IApiCallServies apiCallServies)
        {
            _apiCallServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }

        #region AddFoundeWallet

        [Route("PayWallet")]
        [Authorize]
        public async Task<IActionResult> AddFoundeWallet()
        {
            var userName = User.Identity.Name.ToString();

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUBN(V1)", userName);

            var responseWalletUserMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/FWUBN(V1)", userName);

            if (responseMessage.IsSuccessStatusCode && responseWalletUserMessage.IsSuccessStatusCode)
            {
                var respons = await responseMessage.Content.ReadAsStringAsync();
                var responseWalletUser = await responseWalletUserMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<WalletViewModel>(respons);
                var walletAmount = JsonConvert.DeserializeObject<int>(responseWalletUser);
                user.Amount = walletAmount;

                return View(user);
            }

            return NotFound();
        }

        [HttpPost("PayWallet")]
        [Authorize]
        public async Task<IActionResult> AddFoundeWallet(WalletViewModel user)
        {
            var userName = User.Identity.Name.ToString();

            if (!ModelState.IsValid)
                return View(ModelState);

            var data = new
            {
                UserName = userName,
                Amount = user.Amount,
            };

            var responseWalletUserMessage = await _apiCallServies.SendPostReauest($"https://localhost:7205/api/UserAccount(V1)/AFUW(V1)", data);

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUBN(V1)", userName);

            if (responseMessage.IsSuccessStatusCode && responseWalletUserMessage.IsSuccessStatusCode)
            {
                var respons = await responseMessage.Content.ReadAsStringAsync();
                var responseWalletUse = await responseWalletUserMessage.Content.ReadAsStringAsync();

                var userExixst = JsonConvert.DeserializeObject<WalletViewModel>(respons);
                var amount = JsonConvert.DeserializeObject<int>(responseWalletUse);

                userExixst.Amount += amount;

                return View(userExixst);
            }

            return View(user);
        }

        #endregion
    }
}

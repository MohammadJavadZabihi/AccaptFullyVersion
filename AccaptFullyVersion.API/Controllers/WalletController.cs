using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.API.Controllers
{
    [Route("api/UserAccount(V1)")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IUserServies _userServies;
        private readonly IMapper _mapper;
        private readonly IWalletServies _walletServies;

        public WalletController(
            IUserServies userServies,
            IMapper mapper,
            IWalletServies walletServies)
        {
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _walletServies = walletServies ?? throw new ArgumentNullException(nameof(walletServies));
        }

        #region Get UserWallet by UserName

        [HttpPost("FWUBN(V1)")]
        public async Task<IActionResult> GetUserWallet([FromBody] string userName)
        {
            if (userName == null)
                return BadRequest("User Is Null");

            var userWallet = await _walletServies.FindeWalletWithUserName(userName);

            if (userWallet == null)
                return BadRequest("UnSuccessfully for finding wallet");

            return Ok(userWallet.Amount);
        }

        #endregion

        #region AddFoundWallet 

        [HttpPost("AFUW(V1)")]
        public async Task<IActionResult> AddFoundWallet(WalletViewModel wallet)
        {
            if (wallet.Amount == 0)
                return BadRequest();

            var addwallet = await _walletServies.AddFoundWallet(wallet.Amount, wallet.UserName);

            if (addwallet == null)
                return BadRequest("wallet is null");

            return Ok(wallet.Amount);
        }

        #endregion
    }
}

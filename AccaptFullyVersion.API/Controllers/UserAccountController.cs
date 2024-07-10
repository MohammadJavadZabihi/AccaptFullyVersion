using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.API.Controllers
{
    [Route("api/UserAccount(V1)")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        #region Injection

        private readonly IUserServies _userServies;
        public UserAccountController(IUserServies userServies)
        {
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
        }

        #endregion

        #region Register User Account

        [HttpPost]
        [Route("RUA(V1)")]
        public async Task<IActionResult> RegisterUser(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest("User Model Is NULL");

            if (await _userServies.IsExistUserName(user.UserName))
                return BadRequest("This UserName Is Not Available at This Time, Pleas Select The Difrent UserName");

            if (await _userServies.IsExistEmailAddress(user.Email))
                return BadRequest("This Email Has been already Registered");

            var userRegisterStatuce = await _userServies.RegisterUser(user);

            if (!userRegisterStatuce)
                return BadRequest("The Register Operation is not successfully");

            return Ok("User Register Successfully");
        }

        #endregion

        #region Login User Account

        [HttpPost]
        [Route("LUA(V1)")]
        public async Task<IActionResult> LoginUser(UserLoginViewModel user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest("User Model Is NULL");

            var userLoginStatuce = await _userServies.LoginUser(user);

            if (!userLoginStatuce)
                return BadRequest("Email Or Password is Wrong");

            var exsistUser = await _userServies.FindeUSerByEmail(user.Email);

            if (!exsistUser.IsActive)
                return BadRequest("Your Account is not activated");

            return Ok(new
            {
                UserLoginStatuce = "The User Has been Login Successfully",
                UserEmail = user.Email
            });
        }

        #endregion

        #region Get User For UserPannel

        [Route("GUINF(V1)")]
        [HttpPost]
        public async Task<IActionResult> GetUserInfo(InformationUserViewModel info)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userServies.GetUserInfo(info.Email);
            if (user == null)
                return BadRequest("nulluser");

            return Ok(user);
        }

        #endregion
    }
}

using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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
                return BadRequest("UserName Or Password is Wrong");

            var exsistUser = await _userServies.FindeUserByeUserName(user.UserName);

            if (!exsistUser.IsActive)
                return BadRequest("Your Account is not activated");

            return Ok(new
            {
                UserLoginStatuce = "The User Has been Login Successfully",
                UserEmail = user.UserName
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

            var user = await _userServies.GetUserInfo(info.UserName);
            if (user == null)
                return BadRequest("nulluser");

            return Ok(user);
        }

        #endregion

        #region Find User by User Name

        [Route("GUBN(V1)")]
        [HttpPost]
        public async Task<IActionResult> GetUserByName([FromBody]string username)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(username == null)
                return BadRequest("UserName is null");

            var user = _userServies.FindeUserByeUserName(username);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        #endregion

        #region Patch Update User

        [HttpPut]
        [Route("UPD(V1)")]
        public async Task<IActionResult> UpdatedUser(UserUpdateAccountViewModel userUPD)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userServies.UpdateUser(userUPD);

            if (user == null)
                return BadRequest("User is null");

            return Ok(user);
        }

        #endregion
    }
}

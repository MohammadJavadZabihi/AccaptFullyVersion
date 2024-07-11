using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using AutoMapper;

namespace AccaptFullyVersion.API.Controllers
{
    [Route("api/UserAccount(V1)")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        #region Injection

        private readonly IUserServies _userServies;
        private readonly IMapper _mapper;
        public UserAccountController(IUserServies userServies, IMapper mapper)
        {
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        [HttpPatch]
        [Route("UPD(V1){userName}")]
        public async Task<IActionResult> UpdatedUser(string userName, [FromBody] JsonPatchDocument<UserUpdateAccountViewModel> patchDocument)
        {
            if (patchDocument == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userServies.FindeUserByeUserName(userName);

            if (user == null)
                return NotFound();

            var usertToPatch = _mapper.Map<UserUpdateAccountViewModel>(user);

            patchDocument.ApplyTo(usertToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(usertToPatch, user);
            _userServies.Save();

            return Ok(user);
        }


        #endregion
    }
}

﻿using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Entites;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Security.Claims;

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
            if (!ModelState.IsValid)
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

        #region Login User in Web Site

        [Route("Login")]
        public IActionResult LoginUserAccount()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginUserAccount(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var data = new
            {
                UserName = user.UserName,
                Password = user.Password,
            };

            var response = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/LUA(V1)", data);

            if (response.IsSuccessStatusCode)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = user.RememberMe
                };

                HttpContext.SignInAsync(principal, properties);

                ViewBag.IsSucces = true;
                return Redirect("UserPannel");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    ModelState.AddModelError(string.Empty, $"{responseContent}");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while LoginingUser. Please try again later.");
                    return View(user);
                }
            }
        }

        #endregion

        #region LogOut

        [Route("Logout")]
        public IActionResult LogOutingAccount()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region User Pannel

        [Route("UserPannel")]
        [Authorize]
        public async Task<IActionResult> UserPannel()
        {
            var data = new
            {
                UserName = User.Identity.Name.ToString(),
                Email = "mahan@gmail.com"
            };

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUINF(V1)", data);

            if (responseMessage.IsSuccessStatusCode)
            {
                var respons = await responseMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<InformationUserViewModel>(respons);

                return View(user);
            }

            return NotFound();
        }

        #endregion

        #region Edite User Profile

        [Route("UpdateUser")]
        public IActionResult PatchUserUpdate()
        {
            return View();
        }


        [Route("UpdateUser")]
        [HttpPatch]
        public async Task<IActionResult> PatchUserUpdate(UserUpdateAccountViewModel userUP)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var dataUser = new
            {
                UserName = User.Identity.Name.ToString(),
                Email = "mahan@gmail.com"
            };

            var userExist = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUBN(V1)", dataUser);

            if(userExist.IsSuccessStatusCode)
            {
                var response = await userExist.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(response);

                var data = await _apiCallServies.SendPatchRequest($"https://localhost:7205/api/UserAccount(V1)/UPD(V1)/{User.Identity.Name}", userUP);

                if (data.IsSuccessStatusCode)
                    return View(user);
                else
                    return View();
            }

            return View();

        }

        #endregion
    }
}

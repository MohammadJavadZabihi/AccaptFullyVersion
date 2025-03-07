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
            var userName = User.Identity.Name.ToString();

            var data = new
            {
                UserName = userName,
                Email = "mahan@gmail.com"
            };

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUINF(V1)", data);

            var responseWalletUserMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/FWUBN(V1)", userName);

            if (responseMessage.IsSuccessStatusCode && responseWalletUserMessage.IsSuccessStatusCode)
            {
                var respons = await responseMessage.Content.ReadAsStringAsync();
                var responseWalletUser = await responseWalletUserMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<InformationUserViewModel>(respons);
                var wallet = JsonConvert.DeserializeObject<Wallet>(respons);

                var walletAmount = wallet.Amount;
                user.Wallet = walletAmount;

                return View(user);
            }

            return NotFound();
        }

        #endregion

        #region Edite User Profile

        [Route("UserPannel/EditeUserPannel")]
        public async Task<IActionResult> UpdateUserProfile()
        {
            var userName = User.Identity.Name;

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUBN(V1)", userName);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<UserUpdateAccountViewModel>(response);

                return View(user);
            }

            return NotFound();
        }

        [HttpPost("UserPannel/EditeUserPannel")]
        public async Task<IActionResult> UpdateUserProfile(UserUpdateAccountViewModel userUPD)
        {
            if (!ModelState.IsValid)
            {
                return View(userUPD);
            }

            var patchData = new[]
            {
                new { op = "replace", path = "UserName", value = userUPD.UserName },
                new { op = "replace", path = "Email", value = userUPD.Email }
            };

            var responseMessage = await _apiCallServies.SendPatchRequest($"https://localhost:7205/api/UserAccount(V1)/UPD(V1){User.Identity.Name}", patchData);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(response);

                return Redirect("/Logout");
            }

            return View(ModelState);
        }
        #endregion
    }
}

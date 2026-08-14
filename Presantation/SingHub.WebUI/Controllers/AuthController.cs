using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SingHub.Dto.AuthDtos;
using SingHub.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SingHub.WebUI.Controllers
{
    public class AuthController(IHttpClientFactory _httpClient) : Controller
    {
        private readonly HttpClient _client = _httpClient.CreateClient("SingHubAPI");

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _client.PostAsJsonAsync("users/register", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            await GetErrors(response);
            return View(dto);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _client.PostAsJsonAsync("users/login", dto);
            if (!response.IsSuccessStatusCode)
            {
                await GetErrors(response);
                return View(dto);
            }


            var result = await response.Content.ReadFromJsonAsync<GetJwtTokenInfo>();
            if (result?.Data?.Token != null)
            {
                var tokenString = result.Data.Token;
                var expirationTime = result.Data.ExpirationTime;
                var handler = new JwtSecurityTokenHandler();
                var jwtTokenDetails = handler.ReadJwtToken(tokenString);


                var claims = jwtTokenDetails.Claims.ToList();

                claims.Add(new Claim("AccessToken", tokenString));


                var claimsIdentity = new ClaimsIdentity
                (
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    nameType: JwtRegisteredClaimNames.UniqueName,
                    roleType: "role"
                );

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = expirationTime
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
            }

            return RedirectToAction("HomePage", "Index", new { Area = "Users" });
        }
        #endregion

        #region ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            TempData["Email"] = dto.Email;
            var response = await _client.PostAsJsonAsync("users/forgotpassword", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("ResetPassword");

            await GetErrors(response);
            return View(dto);
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword()
        {
            if (TempData["Email"] != null)
                ViewBag.Email = TempData["Email"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var response = await _client.PostAsJsonAsync("users/resetpassword", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            await GetErrors(response);
            return View(dto);
        }
        #endregion

        #region GetErrorsMethod
        private async Task GetErrors(HttpResponseMessage response)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponseError>();
            if (result?.Errors != null)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Message ?? "An error occurred.");
                }
            }
        }
        #endregion
    }
}

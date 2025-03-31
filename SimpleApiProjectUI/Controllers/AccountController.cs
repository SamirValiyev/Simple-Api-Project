using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SimpleApiProjectUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SimpleApiProjectUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var content=new StringContent(JsonSerializer.Serialize(userLogin),Encoding.UTF8, "application/json");

               var result=await client.PostAsync("http://localhost:5128/api/Auth/Login",content);
                if (result.IsSuccessStatusCode)
                {
                    var jsonData=await result.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData,new JsonSerializerOptions
                    {
                        PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                    });

                    if (tokenModel != null)
                    {
                        JwtSecurityTokenHandler tokenHandler = new();
                        var token=tokenHandler.ReadJwtToken(tokenModel.AccessToken);

                        var claimsIdentity=new ClaimsIdentity(token.Claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                      //  var claims = new ClaimsPrincipal(tokenModel.AccessToken,)
                       await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProps);

                        return RedirectToAction("Index", "Home");   
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
                return View();
            }
            return View(userLogin);

        }
    }
}

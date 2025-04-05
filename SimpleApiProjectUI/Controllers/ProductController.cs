using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleApiProjectUI.Models;
using System.Text.Json;

namespace SimpleApiProjectUI.Controllers
{
    [Authorize(Roles ="Admin,Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetProducts()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var response = await client.GetAsync("http://localhost:5128/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData= await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductResponseModel>>(jsonData,new JsonSerializerOptions
                    {
                        PropertyNamingPolicy= JsonNamingPolicy.CamelCase    
                    });
                    return View(result);
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var token = User.Claims.FirstOrDefault(x=>x.Type=="accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var response = await client.GetAsync($"http://localhost:5128/api/products");
            }
            return View();
        }
    }
}

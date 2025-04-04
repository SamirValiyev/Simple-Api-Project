using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using SimpleApiProjectUI.Models;
using System.Text.Json;

namespace SimpleApiProjectUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetCategories()
        {
            
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,token);
                var response = await client.GetAsync("http://localhost:5128/api/categories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData= await response.Content.ReadAsStringAsync();
                    var result= JsonSerializer.Deserialize<List<CategoryResponseModel>>(jsonData,new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);
                }

            }

            return View();
        }
    }
}

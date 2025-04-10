using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleApiProjectUI.Models;
using System.Text;
using System.Text.Json;

namespace SimpleApiProjectUI.Controllers
{
    [Authorize(Roles = "Admin,Member")]
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
            if (token is not null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var response = await client.GetAsync("http://localhost:5128/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductResponseModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var responseProduct = await client.GetAsync($"http://localhost:5128/api/products/{id}");
                if (responseProduct.IsSuccessStatusCode)
                {
                    var jsonData = await responseProduct.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductRequestModel>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    var responseCategory = await client.GetAsync($"http://localhost:5128/api/categories");
                    if(responseCategory is not null)
                    {
                        var jsonCategoryData=await responseCategory.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<List<CategoryResponseModel>>(jsonCategoryData, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                        if (result is not null)
                            result.Categories = new SelectList(data, "Id", "Name");

                    }
                    return View(result);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductRequestModel model)
        { 
            var data = TempData["Categories"]?.ToString();
            if(data is not null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                model.Categories=new SelectList(categories,"Value","Text",model.CategoryId);
            }
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token is not null)
                {
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                    var jsonData = JsonSerializer.Serialize(model);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("http://localhost:5128/api/products", content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("GetProducts", "Product");
                    else
                        ModelState.AddModelError(String.Empty, "Error occurred while updating product");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var response = await client.DeleteAsync($"http://localhost:5128/api/products/{id}");
            }
            return RedirectToAction("GetProducts");
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ProductRequestModel();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token is not null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await client.GetAsync("http://localhost:5128/api/categories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData=await response.Content.ReadAsStringAsync();    
                    var data=JsonSerializer.Deserialize<List<CategoryResponseModel>>(jsonData,new JsonSerializerOptions
                    {
                        PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                    });
                    model.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Id", "Name");
                    return View(model);
                }
            }
            return RedirectToAction("GetProducts");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestModel model)
        {
            var data = TempData["Categories"]?.ToString();
            if(data is not null)
            {
                var categories=JsonSerializer.Deserialize<List<SelectListItem>>(data);
                model.Categories=new SelectList(categories,"Value", "Text");
                if (ModelState.IsValid)
                {
                    var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                   // var token = Request.Cookies["accesToken"];
                    if (token is not null)
                    {
                        var client = _httpClientFactory.CreateClient();
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                        var jsonData = JsonSerializer.Serialize(model);
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync($"http://localhost:5128/api/products", content);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction("GetProducts");
                        else
                            ModelState.AddModelError(String.Empty, "Error occurred while creating product");
                    }
                }
            }
            return View(model);
        }
    }
}

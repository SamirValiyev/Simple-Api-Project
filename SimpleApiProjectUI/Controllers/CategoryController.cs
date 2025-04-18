﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleApiProjectUI.Models;
using System.Text;
using System.Text.Json;

namespace SimpleApiProjectUI.Controllers
{
    [Authorize(Roles ="Admin")]
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
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
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

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var response=await client.DeleteAsync($"http://localhost:5128/api/categories/{id}");
                return RedirectToAction("GetCategories");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new CategoryRequestModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                    var jsonData = JsonSerializer.Serialize(model);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:5128/api/categories", content);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("GetCategories");
                    else
                       ModelState.AddModelError(String.Empty, "Error occurred while creating category");    

                }
            }
            return View(model);
          
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data=new CategoryRequestModel();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if(token is not null)
            {
                var client=_httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,token);
                var response = await client.GetAsync($"http://localhost:5128/api/categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData=await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<CategoryResponseModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    data.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(result, "Id", "Name");
                    return View(result);
                }


            }
            return View();  

        }
        [HttpPost]  
        public async Task<IActionResult> Update(CategoryRequestModel model)
        {
            var data = TempData["Categories"]?.ToString();
            if(data is not null)
            {
                var categories = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                model.Categories = new SelectList(categories, "Value", "Text");
                if (ModelState.IsValid)
                {
                   
                    var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                    if (token is not null)
                    {
                        var client = _httpClientFactory.CreateClient();
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                        var jsonData = JsonSerializer.Serialize(model);
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        var response = await client.PutAsync("http://localhost:5128/api/categories", content);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction("GetCategories", "Category");
                        else
                            ModelState.AddModelError(String.Empty, "Error occurred while updating category");
                    }

                }
            }
            return View();
        }

    }
}

﻿using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public BlogHttpClientController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("RestApiUrl").Value!); // use this cuz program.cs mhr baseAddress no declare lote htr lh
        }

        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            HttpResponseMessage response = await _httpClient.GetAsync("api/blog");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(JsonStr)!;
            }
            return View("~/Views/BlogRefit/Index.cshtml", model);
        }
    }
}
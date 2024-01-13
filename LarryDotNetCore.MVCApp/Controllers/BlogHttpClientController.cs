using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            HttpResponseMessage response = await _httpClient.GetAsync("api/blog");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(JsonStr)!;
            }
            return View("BlogIndex", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            string JsonBlog = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(JsonBlog, Encoding.UTF8, Application.Json);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("api/blog/", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr)!;
            }
            return Redirect("/bloghttpclient");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                return View("BlogEdit", model);
            }
            return Redirect("/bloghttpclient");
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            string JsonBlog = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(JsonBlog, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.PutAsync($"/api/blog/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
            }
            return Redirect("/bloghttpclient");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
            }
            return Redirect("/bloghttpclient");
        }
    }
}

using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            BlogResponseModel model = new BlogResponseModel();
            RestRequest request = new RestRequest("/api/blog", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr)!;
            }
            return View("Index", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            RestRequest request = new RestRequest("/api/blog", Method.Post);
            request.AddJsonBody(reqModel);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
            }
            return Redirect("/blogrestclient");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            RestRequest request = new RestRequest($"/api/blog/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                return View("BlogEdit", model);
            }
            return Redirect("/blogrestclient");
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            RestRequest request = new RestRequest($"/api/blog/{id}", Method.Put);
            request.AddJsonBody(reqModel);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
            }
            return Redirect("/blogrestclient");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            RestRequest request = new RestRequest($"/api/blog/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
            }
            return Redirect("/blogrestclient");
        }
    }
}

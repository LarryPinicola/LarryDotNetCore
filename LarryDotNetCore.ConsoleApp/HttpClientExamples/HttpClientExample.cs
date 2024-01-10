using Azure;
using LarryDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LarryDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            await Read();
            await Edit(9);
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7091/api/blog");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogListResponseModel>(JsonStr);
                foreach (var blog in model!.Data)
                {
                    Console.WriteLine(blog.Blog_Id);
                    Console.WriteLine(blog.Blog_Title);
                    Console.WriteLine(blog.Blog_Author);
                    Console.WriteLine(blog.Blog_Content);
                }
            }
        }

        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7091/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                var blog = model!.Data;
                Console.WriteLine(blog.Blog_Id);
                Console.WriteLine(blog.Blog_Title);
                Console.WriteLine(blog.Blog_Author);
                Console.WriteLine(blog.Blog_Content);
            }
            else
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                Console.WriteLine(model.Message);
            }
        }

        public async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://localhost:7091/api/blog/", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                await Console.Out.WriteLineAsync(model.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Azure;
using LarryDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace LarryDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        public async Task Run()
        {
            await Read();
            await Edit(9);
            await Create("ookay", "six", "text");
            await Update(5, "let", "me", "in");
        }

        public async Task Read()
        {
            RestRequest request = new RestRequest("https://localhost:7091/api/blog", Method.Get);
            RestClient client = new RestClient();
            // await client.GetAsync(request);  // GetAsync တိုက်ရိုက်သုံးခြင်း
            var response = await client.ExecuteAsync(request); // execute ကို ာာmethod ပေးပြီးသုံးတယ် 
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content; // no await and async unlike httpclient
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
            RestRequest request = new RestRequest($"https://localhost:7091/api/blog/{id}", Method.Get);
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                var blog = model!.Data;
                Console.WriteLine(blog.Blog_Id);
                Console.WriteLine(blog.Blog_Title);
                Console.WriteLine(blog.Blog_Author);
                Console.WriteLine(blog.Blog_Content);
            }
            else
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                Console.WriteLine(model!.Message);
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
            RestRequest request = new RestRequest("https://localhost:7091/api/blog/", Method.Post);
            request.AddJsonBody(blog); // add new content into request
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                await Console.Out.WriteLineAsync(model!.Message);
            }
        }

        public async Task Update(int id, string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            RestRequest request = new RestRequest($"https://localhost:7091/api/blog/{id}", Method.Put);
            request.AddJsonBody(blog); // add new content into request
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                await Console.Out.WriteLineAsync(model!.Message);
            }
            else
            {
                string jsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model!.Message);
            }
        }

        public async Task Delete(int id)
        {
            RestRequest request = new RestRequest("https://localhost:7091/api/blog/", Method.Delete);
            RestClient client = new RestClient();
            //var response = await client.DeleteAsync(request);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                await Console.Out.WriteLineAsync(model!.Message);
            }
            else
            {
                string JsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(JsonStr);
                await Console.Out.WriteLineAsync(model!.Message);
            }
        }
    }

}

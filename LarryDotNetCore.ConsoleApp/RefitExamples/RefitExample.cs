using LarryDotNetCore.ConsoleApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LarryDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi blogApi = RestService.For<IBlogApi>("https://localhost:7091/api/blog/");

        public async Task Run()
        {
            await Read();
            await Edit(4);
            await Create("home", "mountain", "road");
            await Update(5, "sea", "nature", "light");
            await Delete(5);
        }

        private async Task Read()
        {
            var model = await blogApi.GetBlogs();
            foreach (var blog in model!.Data)
            {
                Console.WriteLine(blog.Blog_Id);
                Console.WriteLine(blog.Blog_Title);
                Console.WriteLine(blog.Blog_Author);
                Console.WriteLine(blog.Blog_Content);
            }
        }

        private async Task Edit(int id)
        {
            var model = await blogApi.GetBlog(id);
            var blog = model!.Data;
            Console.WriteLine(blog.Blog_Id);
            Console.WriteLine(blog.Blog_Title);
            Console.WriteLine(blog.Blog_Author);
            Console.WriteLine(blog.Blog_Content);
        }

        private async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            var model = await blogApi.CreateBlog(blog);
            await Console.Out.WriteLineAsync(model.Message);
        }

        private async Task Update(int id, string title, string author, string content)
        {
            /*BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };*/ // another way of inserting an Object
            //BlogResponseModel model = await blogApi.UpdateBlog(id, blog);
            BlogResponseModel model = await blogApi.UpdateBlog(id, new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });
            await Console.Out.WriteLineAsync(model.Message);
        }

        private async Task Delete(int id)
        {
            var model = await blogApi.DeleteBlog(id);
            await Console.Out.WriteLineAsync(model!.Message);
        }
    }
}

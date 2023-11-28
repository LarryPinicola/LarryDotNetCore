using LarryDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LarryDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDBContext _dbContext;

        public EFCoreExample()
        {
            _dbContext = new AppDBContext();
        }

        public void Run()
        {
            Read();
            //Edit(1);
            //Create("Apple", "SteveJobs", "Laptop");
            //Update(4, "update", "update", "update");
            //Delete(10);
        }

        #region Read
        private void Read()
        {
            List<BlogDataModel> lst = _dbContext.Blogs.ToList();
            foreach (var blog in lst)
            {
                Console.WriteLine(blog.Blog_Id);
                Console.WriteLine(blog.Blog_Title);
                Console.WriteLine(blog.Blog_Author);
                Console.WriteLine(blog.Blog_Content);
            }
        }
        #endregion

        #region Edit
        private void Edit(int id)
        {
            //BlogDataModel? blog = _dbContext.Blogs.Where(x=>x.Blog_Id == id).FirstOrDefault();
            BlogDataModel? blog = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("no data found");
                return;
            }
            Console.WriteLine(blog.Blog_Id);
            Console.WriteLine(blog.Blog_Title);
            Console.WriteLine(blog.Blog_Author);
            Console.WriteLine(blog.Blog_Content);
        }
        #endregion

        #region Create
        private void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            };
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "successfully created" : "create fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Update
        private void Update(int id, string title, string author, string content)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(blog is null)
            {
                Console.WriteLine("no data found");
                return;
            }
            blog.Blog_Title = title;
            blog.Blog_Author = author;
            blog.Blog_Content = content;
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "successfully updated" : "update fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Delete
        private void Delete(int id)
        {
            var blog =  _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("no data found");
                return;
            }
            _dbContext.Blogs.Remove(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "successfully deleted" : "delete fail";
            Console.WriteLine(message);
        }
        #endregion
    }
}

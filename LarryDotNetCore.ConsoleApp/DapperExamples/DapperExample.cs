using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LarryDotNetCore.ConsoleApp.Models;

namespace LarryDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public DapperExample()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-47QMMLG",
                InitialCatalog = "Tbl_Blog",
                UserID = "sa",
                Password = "sa@123"
            };
        }

        public void Run()
        {
            Read();
            Edit(1);
            Create("text1", "text1", "text1");
            Update(1, "newText", "newText", "newText");
            Delete(1);
        }

        #region Read
        private void Read()
        {
            string query = "SELECT * FROM Tbl_Blog";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }
        #endregion

        #region Edit
        private void Edit(int id)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id
            };
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            //List<dynamic> lst = db.Query(query).ToList();
            //List<BlogDataModel> lst = db.Query<BlogDataModel>(query, blog).ToList();
            BlogDataModel? item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("no data found");
                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
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
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "successfully created" : "create fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Update
        private void Update(int id, string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] =@Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "successfully updated" : "update fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Delete
        private void Delete(int id)
        {
            BlogDataModel blog = new BlogDataModel { Blog_Id = id };
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "successfully deleted" : "delete fail";
            Console.WriteLine(message);
        }
        #endregion
    }
}

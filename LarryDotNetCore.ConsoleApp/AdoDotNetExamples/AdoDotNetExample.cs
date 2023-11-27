using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace LarryDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public AdoDotNetExample()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "LarryDotNetCore",
                UserID = "sa",
                Password = "sa@123",
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
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Blog_Id"]);
                Console.WriteLine(row["Blog_Title"]);
                Console.WriteLine(row["Blog_Author"]);
                Console.WriteLine(row["Blog_Content"]);
            }
        }
        #endregion

        #region Edit
        private void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("no data found");
                return;
            }
            DataRow row = dt.Rows[0];
            Console.WriteLine(row["Blog_Id"]);
            Console.WriteLine(row["Blog_Title"]);
            Console.WriteLine(row["Blog_Author"]);
            Console.WriteLine(row["Blog_Content"]);
        }
        #endregion

        #region Create
        private void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "successfully created" : "create fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Update
        private void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] =@Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "successfully updated" : "update fail";
            Console.WriteLine(message);
        }
        #endregion

        #region Delete
        private void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "successfully deleted" : "delete fail";
            Console.WriteLine(message);
        }
        #endregion
    }
}

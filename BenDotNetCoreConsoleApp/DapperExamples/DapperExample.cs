using BenDotNetCoreConsoleApp.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDotNetCoreConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperExample()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "BenDotNetCore",
                UserID = "sa",
                Password = "sasa@123",
            };
        }

        public void Run()
        {
            Create("Test Dapper", "Dapper Title", "Dapper Content");
            Retrieve();
            Update(3, "Updated Author", "Updated Title", "Updated Content");
            Delete(4);
        }

        private void Create(string author, string title, string content)
        {
            string query = @"INSERT INTO [dbo].[TblBlogs] VALUES (
                            @BlogAuthor,
                            @BlogTitle,
                            @BlogContent
                            )";
            BlogDto blog = new BlogDto {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Success" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Retrieve()
        {
            string query = "SELECT * FROM TblBlogs";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var lists = db.Query<BlogDto>(query).ToList();
            foreach (var blog in lists)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine("--------------------");
            }
        }

        private void Update(int id, string author, string title, string content)
        {
            string query = @"UPDATE [dbo].[TblBlogs] SET
                            BlogTitle = @BlogTitle,
                            BlogAuthor = @BlogAuthor,
                            BlogContent = @BlogContent
                            WHERE BlogId = @BlogId";
            BlogDto blog = new BlogDto
            {
                BlogId = id,
                BlogAuthor = author,
                BlogTitle = title,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update Success" : "Update Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[TblBlogs] WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new { BlogId = id });
            string message = result > 0 ? "Delete Success" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}

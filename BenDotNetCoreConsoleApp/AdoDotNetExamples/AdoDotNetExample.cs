using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDotNetCoreConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        public AdoDotNetExample()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "BenDotNetCore",
                UserID = "sa",
                Password = "sasa@123",
            };
        }

        public void RunAdoDotNet()
        {
            Create("someone", "something", "every reason causes every problem");
            Retrieve();
            Update(2, "updated_author", "updated_title", "updated_content");
            Delete(2);
        }

        private void Create(string author, string title, string content)
        {
            string query = @"INSERT INTO [dbo].[TblBlogs] VALUES (
                            @BlogAuthor,
                            @BlogTitle,
                            @BlogContent
                            )";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving Success" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Retrieve()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TblBlogs", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogContent"]);
                Console.WriteLine("------------------");
            }

        }

        private void Update(int id, string author, string title, string content)
        {
            string query = @"UPDATE [dbo].[TblBlogs] SET
                            BlogTitle = @BlogTitle,
                            BlogAuthor = @BlogAuthor,
                            BlogContent = @BlogContent
                            WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogId", id);
            try
            {
                int result = cmd.ExecuteNonQuery();
                string message = result > 0 ? "Update Success" : "Update Failed";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }

        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[TblBlogs] WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            try
            {
                int result = cmd.ExecuteNonQuery();
                string message = result > 0 ? "Delete Success" : "Delete Failed";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }

        }
    }
}

using BenDotNetCoreConsoleApp.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BenDotNetCoreConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _db = new AppDbContext();

        public void Run()
        {
            // CRUD CAll
            Create("EFCore", "EFCORE", "About EFCore");
            Retrieve();

        }

        private void Create(string author, string title, string content)
        {
            BlogDto blog = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            _db.Add(blog);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Saving Success" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogContent);
        }

        private void Retrieve()
        {
            var lists = _db.Blogs.ToList();
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
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("Blog is not found");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            _db.SaveChanges();
        }

        private void Delete(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            _db.Blogs.Remove(item);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Delete Success" : "Delete Failed";
            Console.WriteLine(message);
        }

    }


}

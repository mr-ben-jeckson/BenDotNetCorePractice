using BenDotNetCore.RESTapi.Database;
using BenDotNetCore.RESTapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BenDotNetCore.RESTapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult Get()
        {
            var blogs = _db.Blogs
                    .Where(x => x.IsDeleted == false)
                    .ToList();
            return Ok(blogs);
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Saving Success" : "Saving Fail";
            return Ok(message);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
            if (blog is null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Blog blog)
        {
            var updatedBlog = _db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
            if(updatedBlog is null)
            {
                return NotFound("Data Not Found");
            }
            updatedBlog.BlogAuthor = blog.BlogAuthor;
            updatedBlog.BlogTitle = blog.BlogTitle;
            updatedBlog.BlogContent = blog.BlogContent;
            _db.SaveChanges();
            return Ok(updatedBlog);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Blog blog)
        {
            var patchedBlog = _db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
            if (patchedBlog is null)
            {
                return NotFound("Data Not Found");
            }
            if(!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                patchedBlog.BlogAuthor = blog.BlogAuthor;
            }
            if(!String.IsNullOrEmpty(blog.BlogTitle))
            {
                patchedBlog.BlogTitle= blog.BlogTitle;
            }
            if(!String.IsNullOrEmpty(blog.BlogContent))
            {
                patchedBlog.BlogContent= blog.BlogContent;
            }
            _db.SaveChanges();

            return Ok(patchedBlog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeBlog = _db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
            if(removeBlog is null)
            {
                return NotFound("Data Not Found");
            }
            removeBlog.IsDeleted = true;
            var result = _db.SaveChanges();
            var message = result > 0 ? "Remove Success" : "Remove Fail";
            return Ok(message);
        }
    }
}

using BenDotNetCoreMinimalApi.Database;
using BenDotNetCoreMinimalApi.Models;
using Microsoft.AspNetCore.Builder;

namespace BenDotNetCoreMinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlog(this IEndpointRouteBuilder app)
        {
            //Get Lists
            app.MapGet("/api/blog", () =>
            {
                AppDbContext db = new AppDbContext();
                var blogs = db.Blogs
                    .Where(x => x.IsDeleted == false)
                    .ToList();
                return Results.Ok(blogs);
            });
            //Get Single
            app.MapGet("/api/blog/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var blog = db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
                if (blog is null)
                {
                    return Results.NotFound("Data Not Found");
                }
                return Results.NotFound(blog);
            });
            //Create
            app.MapPost("/api/blog", (BlogModel blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.Blogs.Add(blog);
                var result = db.SaveChanges();
                string message = result > 0 ? "Saving Success" : "Saving Fail";
                return Results.Ok(message);
            });
            //Update
            app.MapPut("/api/blog/{id}", (int id, BlogModel blog) =>
            {
                AppDbContext db = new AppDbContext();
                var updatedBlog = db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
                if (updatedBlog is null)
                {
                    return Results.NotFound("Data Not Found");
                }
                updatedBlog.BlogAuthor = blog.BlogAuthor;
                updatedBlog.BlogTitle = blog.BlogTitle;
                updatedBlog.BlogContent = blog.BlogContent;
                db.SaveChanges();
                return Results.Ok(updatedBlog);
            });
            //Patch
            app.MapPatch("/api/blog/{id}", (int id, BlogModel blog) =>
            {
                AppDbContext db = new AppDbContext();
                var patchedBlog = db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
                if (patchedBlog is null)
                {
                    return Results.NotFound("Data Not Found");
                }
                if (!String.IsNullOrEmpty(blog.BlogAuthor))
                {
                    patchedBlog.BlogAuthor = blog.BlogAuthor;
                }
                if (!String.IsNullOrEmpty(blog.BlogTitle))
                {
                    patchedBlog.BlogTitle = blog.BlogTitle;
                }
                if (!String.IsNullOrEmpty(blog.BlogContent))
                {
                    patchedBlog.BlogContent = blog.BlogContent;
                }
                db.SaveChanges();

                return Results.Ok(patchedBlog);
            });
            app.MapDelete("/api/blog/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var removeBlog = db.Blogs.Where(x => x.IsDeleted == false && x.BlogId == id).FirstOrDefault();
                if (removeBlog is null)
                {
                    return Results.NotFound("Data Not Found");
                }
                removeBlog.IsDeleted = true;
                var result = db.SaveChanges();
                var message = result > 0 ? "Remove Success" : "Remove Fail";
                return Results.Ok(message);
            });
            return app;
        }
    }
}

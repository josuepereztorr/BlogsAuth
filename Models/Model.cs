using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BlogsAuth.Models
{
    public class Model
    {
        public class BloggingContext : DbContext
        {
            public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }
            
            public void AddBlog(Blog blog)
            {
                this.Add(blog);
                this.SaveChanges();
            }
            
            public void DeleteBlog(Model.Blog blog)
            {
                this.Remove(blog);
                this.SaveChanges();
            }
            
            public void AddPost(Post post)
            {
                this.Add(post);
                this.SaveChanges();
            }
            
            public void DeletePost(Post post)
            {
                this.Remove(post);
                this.SaveChanges();
            }
        }

        public class Blog
        {
            public int BlogId { get; set; }
            [Required]
            public string Name { get; set; }

            public ICollection<Post> Posts { get; set; }
        }

        public class Post
        {
            public int PostId { get; set; }
            [Required]
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public Blog Blog { get; set; }
        }
        
        public class PostViewModel
        {
            public Blog blog { get; set; }
            public IEnumerable<Post> Posts { get; set; }
        }
    }
}
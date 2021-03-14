using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Uploader.Models;
using Uploader.Services;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Uploader.Pages
{
    public class PostModel : PageModel
    {
        public PostService PostSerivce { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public Post Post { get; set; }

        public string PostPath { get; set; }

        public PostModel(PostService postService, IConfiguration configuration)
        {
            PostSerivce = postService;
            Configuration = configuration;
        }

        public IActionResult OnGet([FromQuery] string id)
        {
            if (!string.IsNullOrEmpty(id))
                Post = PostSerivce.Posts.FirstOrDefault(x => x.Id == id);

            if (Post != null)
            {
                PostPath = string.Format("{0}/{1}/{2}.{3}",
                Configuration.GetSection("Server")["Route"], Post.Type, Post.Id, Post.Extension);
                return Page();
            }
            
                

            return RedirectToPage("/index");
        }
    }
}

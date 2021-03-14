using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uploader.Models;
using Uploader.Services;
using System.IO;

namespace Uploader.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        public SignInManager<AccountUser> SignInManager { get; private set; }

        public PostService PostService { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public MainController(SignInManager<AccountUser> signInManager, PostService postService,
            IConfiguration configuration)
        {
            SignInManager = signInManager;
            PostService = postService;
            Configuration = configuration;
        }

        [Route("LogOut")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage("/index");
        }

        [Route("SearchUser")]
        [HttpPost]
        public IActionResult SearchUser([FromForm] string query)
        {
            if (query != null)
            {
                query = query.Replace(" ", "+");
                return RedirectToPage("/search", new { q = query});
            }

            return Redirect("/search");
        }

        [Route("DeletePost")]
        [HttpPost]
        public async Task<IActionResult> DeletePost([FromForm] string id)
        {
            if (!SignInManager.IsSignedIn(User))
                return Redirect("/index");

            var post = PostService.Posts.Where(x => x.Id == id).FirstOrDefault();
            var user = await SignInManager.UserManager.GetUserAsync(User);

            if (post.AccountUser != user)
                return Redirect("/index");

            if (await PostService.RemovePostAsync(post))
            {
                System.IO.File.Delete(Path.Combine(Configuration.GetSection("Server")["Default"],
                    post.Type, string.Format("{0}.{1}", post.Id, post.Extension)));
            }


            return Redirect("/home");


        }

    }
}

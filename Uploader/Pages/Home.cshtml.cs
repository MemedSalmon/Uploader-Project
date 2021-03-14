using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Uploader.Data;
using Uploader.Models;
using Uploader.Services;

namespace Uploader.Pages
{
    public class HomeModel : PageModel
    {
        public SignInManager<AccountUser> SignInManager { get; private set; }

        public PostService PostService { get; private set; }

        public List<Post> Posts { get; set; }

        public HomeModel(SignInManager<AccountUser> signInManager, PostService postService)
        {
            SignInManager = signInManager;
            PostService = postService;
            Posts = new List<Post>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var user = await SignInManager.UserManager.GetUserAsync(User);
                Posts = PostService.Posts.Where(x => x.AccountUser == user).ToList();

                return Page();
            }

            return RedirectToPage("/index");
        }
    }
}

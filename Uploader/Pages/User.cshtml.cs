using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Uploader.Models;
using Uploader.Services;

namespace Uploader.Pages
{
    public class UserModel : PageModel
    {
        public UserManager<AccountUser> UserManager { get; private set; }

        public PostService PostService { get; private set; }

        public List<Post> Posts { get; set; }

        public UserModel(UserManager<AccountUser> userManager, PostService postService)
        {
            UserManager = userManager;
            PostService = postService;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] string user)
        {
            if (string.IsNullOrEmpty(user))
                return RedirectToPage("/index");

            var account = await UserManager.FindByNameAsync(user);

            if (account == null)
                return RedirectToPage("/index");

            Posts = PostService.Posts.Where(x => x.AccountUser == account).ToList();

            return Page();

        }
    }
}

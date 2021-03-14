using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uploader.Data;
using Uploader.Models;
using Uploader.Services;

namespace Uploader.Pages
{
    public class UploadModel : PageModel
    {
        public UserManager<AccountUser> UserManager { get; private set; }

        public SignInManager<AccountUser> SignInManager { get; private set; }

        public PostService PostService { get; private set; }

        public AppDbContext Db { get; private set; }

        public UploadModel(AppDbContext db, UserManager<AccountUser> userManager, 
            SignInManager<AccountUser> signInManger, PostService postService)
        {
            Db = db;
            UserManager = userManager;
            SignInManager = signInManger;
            PostService = postService;
        }

        [ModelBinder]
        public UploadPost Post { get; set; }

        public IActionResult OnGet()
        {
            if (!SignInManager.IsSignedIn(User))
                return Redirect("/");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var post = await PostService.GeneratePostAsync(await PostService.GeneratePostIdAsync(PostService.Posts.Select(x => x.Id)), 
                    Post.Title, Post.TempFile, await UserManager.GetUserAsync(User));

                using FileStream fileStream = new FileStream(Path.Combine("C:\\Server", post.Type, string.Format("{0}.{1}", post.Id, post.Extension)), FileMode.CreateNew, FileAccess.Write);
                
                if (fileStream != null)
                {
                    Db.Add(post);

                    if (Db.SaveChanges() >= 0)
                        await Post.TempFile.CopyToAsync(fileStream);
                    

                    return RedirectToPage("/home");
                }

                ModelState.AddModelError("", "Failed to upload video.");

                return Page();
            }

            return Page();
        }
       

    }
}

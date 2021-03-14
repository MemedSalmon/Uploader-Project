using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Uploader.Models;

namespace Uploader.Pages
{
    public class LogInModel : PageModel
    {
        public SignInManager<AccountUser> SignInManager { get; private set; }

        public LogInModel(SignInManager<AccountUser> signInManager)
        {
            SignInManager = signInManager;
        }

        [ModelBinder]
        public LogInUser LogInUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if ((await SignInManager.PasswordSignInAsync(LogInUser.Username, LogInUser.Password, false, false)).Succeeded)
                {
                    return RedirectToPage("/home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return Page();
        }
    }
}

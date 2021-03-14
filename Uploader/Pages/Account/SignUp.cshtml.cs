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
    public class SignUpModel : PageModel
    {
        public SignInManager<AccountUser> SignInManager { get; private set; }

        public SignUpModel(SignInManager<AccountUser> signInManager)
        {
            SignInManager = signInManager;
        }

        [ModelBinder]
        public RegisterUser RegisterUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                AccountUser user = new AccountUser
                {
                    UserName = RegisterUser.Username
                };

                IdentityResult result = await SignInManager.UserManager.CreateAsync(user, RegisterUser.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);
                    return Redirect("/home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}

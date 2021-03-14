using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uploader.Models;

namespace Uploader.Pages
{
    public class IndexModel : PageModel
    {
        public SignInManager<AccountUser> SignInManager { get; private set; }

        public IndexModel(SignInManager<AccountUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (SignInManager.IsSignedIn(User))
                return Redirect("/home");

            return Page();
        }

    }
}

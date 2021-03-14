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
    public class SearchModel : PageModel
    {
        public UserManager<AccountUser> UserManager { get; private set; }

        public List<List<AccountUser>> Users { get; set; }

        public SearchModel(UserManager<AccountUser> userManager)
        {
            UserManager = userManager;

            Users = new List<List<AccountUser>>
            {
                new List<AccountUser>(),
                new List<AccountUser>(),
                new List<AccountUser>()
            };

        }

        public async Task OnGetAsync([FromQuery] string q)
        {
            if (q != null)
            {
                q = q.Trim();
                q = q.Replace(" ", "+");
                string[] query = q.Split("+");

                for (int i = 0; i < query.Length; i++)
                {
                    AccountUser temp = await UserManager.FindByNameAsync(query[i]);

                    if (temp != null)
                        Users[0].Add(temp);

                }

            }

            Random Rand = new Random();

            Users[2] = UserManager.Users.ToList();

            for (int i = 0; i < Users[0].Count; i++)
            {
                Users[2].RemoveAt(Users[2].IndexOf(Users[0][i]));
            }

            if (Users[2].Count != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    AccountUser randomUser = Users[2][Rand.Next(0, Users[2].Count)];

                    if (!Users[1].Contains(randomUser))
                        Users[1].Add(randomUser);

                }
            }

            
        }

        
    }
}

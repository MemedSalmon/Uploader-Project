using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Uploader.Pages
{
    public class ErrorModel : PageModel
    {
        public int Status { get; set; }

        public ErrorModel()
        {
        }

        public void OnGet([FromQuery] int code)
        {
            Status = code;
        }
    }
}

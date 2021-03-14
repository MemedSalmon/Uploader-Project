using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader.Models
{
    public class UploadPost
    {
        [BindProperty]
        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please choose a file to upload.")]
        public IFormFile TempFile { get; set; }
    }
}

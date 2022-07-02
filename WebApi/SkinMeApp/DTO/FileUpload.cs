using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class FileUpload
    {

        public IFormFile file { get; set; }
        public string AppUser { get; set; }
    }
}
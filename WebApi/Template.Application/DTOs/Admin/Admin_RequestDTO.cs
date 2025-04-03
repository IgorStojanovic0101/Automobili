using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.DTOs.Admin
{
    public class Admin_RequestDTO
    {
        public IFormFile ImageFile { get; set; }
        public string Text { get; set; }
    }
}

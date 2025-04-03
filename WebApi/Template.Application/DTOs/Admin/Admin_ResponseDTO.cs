using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.DTOs.Admin
{
    public class Admin_ResponseDTO
    {
        public string ImageStr { get; set; }
        public string Name { get; set; }
        public string Birthdate { get; set; }
    }
}

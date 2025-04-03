
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.DataModels;

namespace Template.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
       //     modelBuilder.Entity<User>().HasData(
           //    new User() { Id = 1, Name = "Igor", Email = "2232sd", RestoranId = 1, Username = "igor", Password = "123" });

        }
    }
}
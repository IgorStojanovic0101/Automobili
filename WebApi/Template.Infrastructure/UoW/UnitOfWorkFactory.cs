using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Data;
using Template.Domain.UnitOfWork;
using Template.Infrastructure.EF;

namespace Template.Infrastructure.UoW
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _sqlConnectionString;
        private readonly string _mongoConnectionString;
        public UnitOfWorkFactory(IConfiguration configuration)
        {
            _sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            _mongoConnectionString = configuration.GetConnectionString("MongoDB");

        }

      


    }
}

using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Utilities;
using Template.Domain.DataModels;
using Template.Domain.Enums;
using Template.Domain.Repositories;
using Template.Domain.Utilities;
using Template.Infrastructure.EF;

namespace Template.Infrastructure.Repositories
{
    public class AutoRepository : Repository<Auto>, IAutoRepository
    {
        private readonly ApplicationDbContext1 _dbContext;
        private readonly IDbConnection _dbConnection;
        public AutoRepository(ApplicationDbContext1 dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_dbConnection = dbConnection;
        }

        public Task<List<Auto>> GetAllCars()
        {
            return _dbContext.Autos.ToListAsync();
        }


        public async Task<Auto> GetUserById(int id)
        {
           return await this.GetAsync(x => x.Id == id);

        }
    }
}

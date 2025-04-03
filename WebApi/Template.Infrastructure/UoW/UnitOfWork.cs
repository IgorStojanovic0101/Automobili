using Microsoft.EntityFrameworkCore;

using System.Data;
using MongoDB.Driver;
using Template.Domain.Repositories;
using Template.Infrastructure.EF;
using Template.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Template.Infrastructure.UoW
{
    public class UnitOfWork(ApplicationDbContext1 databaseContext, IAutoRepository AutoRepository) : IUnitOfWork
    {

        private readonly ApplicationDbContext1 _applicationDbContext = databaseContext;


        public IAutoRepository AutoRepository { get; private set; } = AutoRepository;




        private IDbContextTransaction? _transaction;


        // Begin a transaction asynchronously
        public async Task BeginTransAsync()
        {

            if (_transaction != null)
                throw new InvalidOperationException("A transaction is already in progress.");

            _transaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }

        // Commit the transaction
        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }



        // Rollback the transaction
        // Rollback the transaction asynchronously
        public async Task RollbackAsync()
        {

            if (_transaction == null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        // Save changes without transaction (if not using transaction)
        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        // Dispose of the DbContext and transaction
        public void Dispose()
        {
            _transaction?.Dispose(); // Dispose of the transaction
            _applicationDbContext?.Dispose();   // Dispose of the DbContext
        }


    }
   

}

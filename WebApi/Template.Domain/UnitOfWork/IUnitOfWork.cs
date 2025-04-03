using Template.Domain.Repositories;

namespace Template.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {

        IAutoRepository AutoRepository { get; }

        Task BeginTransAsync();


        // Commit the transaction
        Task CommitAsync();


        // Rollback the transaction
        // Rollback the transaction asynchronously
        Task RollbackAsync();

        // Save changes without transaction (if not using transaction)

        Task SaveChangesAsync();


        // Dispose of the DbContext and transaction
        void Dispose();
        
    }

  
}

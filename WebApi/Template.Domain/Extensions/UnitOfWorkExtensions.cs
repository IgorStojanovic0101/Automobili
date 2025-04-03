using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.UnitOfWork;

namespace Template.Domain.UnitOfWork
{
    public static class UnitOfWorkExtensions
    {

        public static async Task ExecuteTransactionAsync(this IUnitOfWork unitOfWork, Func<Task> action)
        {

            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            await unitOfWork.BeginTransAsync();

            try
            {
                await action();
                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }

        public static async Task<T> ExecuteTransactionAsync<T>(this IUnitOfWork unitOfWork, Func<Task<T>> action)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            await unitOfWork.BeginTransAsync();
            try
            {
                T result = await action();  // Execute action and get result
                await unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }

        public static async Task<(T1, T2)> ExecuteTransactionAsync<T1, T2>(this IUnitOfWork unitOfWork, Func<Task<(T1, T2)>> action)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            await unitOfWork.BeginTransAsync();
            try
            {
                var result = await action();  // Execute action and get result
                await unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }


        public static async Task<dynamic> ExecuteTransactionAsync(this IUnitOfWork unitOfWork, Func<Task<dynamic>> action)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            await unitOfWork.BeginTransAsync();
            try
            {
                var result = await action();
                await unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
    
}

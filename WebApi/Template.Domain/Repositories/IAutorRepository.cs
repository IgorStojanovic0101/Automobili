using Template.Domain.DataModels;

namespace Template.Domain.Repositories
{
    public interface IAutoRepository : IReadRepository<Auto>, IUpdateRepository<Auto>
    {

        Task<List<Auto>> GetAllCars();
        Task<Auto> GetUserById(int id);


    }
}

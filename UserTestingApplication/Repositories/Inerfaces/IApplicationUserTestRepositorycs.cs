using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface IApplicationUserTestRepository
    {
        Task<IQueryable<ApplicationUserTest>> GetAsync(ApplicationUserTestFilter applicationUserTestFilter = null);
        Task AddAsync(ApplicationUserTest applicationUserTest);
        Task CommitAsync();
        Task DeleteAsync(ApplicationUserTest applicationUserTest);
    }
}

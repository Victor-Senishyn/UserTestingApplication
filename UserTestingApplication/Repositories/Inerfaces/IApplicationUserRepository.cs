using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface IApplicationUserRepository
    {
        Task<IQueryable<ApplicationUser>> GetAsync(ApplicationUserFilter applicationUserFilter = null);
        Task AddAsync(ApplicationUser applicationUser);
        Task CommitAsync();
        Task DeleteAsync(ApplicationUser applicationUser);
    }
}

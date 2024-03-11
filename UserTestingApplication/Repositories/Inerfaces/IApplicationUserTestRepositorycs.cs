using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface IApplicationUserTestRepository
    {
        Task<IQueryable<UserTestResult>> GetAsync(UserTestResultFilter userTestResultFilter = null);
        Task AddAsync(UserTestResult userTestResult);
        Task CommitAsync();
        Task DeleteAsync(UserTestResult userTestResult);
    }
}

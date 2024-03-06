using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface ITestRepository
    {
        Task<IQueryable<Test>> GetAsync(TestFilter testFilter);
        Task AddAsync(Test test);
        Task CommitAsync();
        Task DeleteAsync(Test test);
    }
}

using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface ITestRepository
    {
        Task<IQueryable<Test>> GetAsync(TestFilter testFilter = null);
        Task AddAsync(Test test);
        Task CommitAsync();
        Task DeleteAsync(Test test);
        Task<IEnumerable<Test>> GetTestsAsync();
    }
}

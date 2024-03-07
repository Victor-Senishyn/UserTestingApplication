using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface ICompletedTestRepository
    {
        Task<IQueryable<CompletedTest>> GetAsync(CompletedTestFilter completedTestFilter = null);
        Task AddAsync(CompletedTest completedTest);
        Task CommitAsync();
        Task DeleteAsync(CompletedTest completedTest);
    }
}

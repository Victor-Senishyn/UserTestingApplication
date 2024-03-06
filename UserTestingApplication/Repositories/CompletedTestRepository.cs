using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class CompletedTestRepository : ICompletedTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompletedTestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CompletedTest completedTest)
        {
            await _dbContext.Set<CompletedTest>().AddAsync(completedTest);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CompletedTest completedTest)
        {
            _dbContext.Set<CompletedTest>().Remove(completedTest);
        }

        public async Task<IQueryable<CompletedTest>> GetAsync(CompletedTestFilter completedTestFilter)
        {
            var query = _dbContext.CompletedTests.AsQueryable();

            if (completedTestFilter.Id != null)
                query = query.Where(completedTest => completedTest.Id == completedTestFilter.Id);
            else if (completedTestFilter.UserId != null)
                query = query.Where(completedTest => completedTest.UserId == completedTestFilter.UserId);
            else if (completedTestFilter.TestId != null)
                query = query.Where(completedTest => completedTest.TestId == completedTestFilter.TestId);
            else if (completedTestFilter.Score != null)
                query = query.Where(completedTest => completedTest.Score == completedTestFilter.Score);

            return query;
        }
    }
}

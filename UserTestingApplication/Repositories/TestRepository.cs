using Microsoft.EntityFrameworkCore;
using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Test test)
        {
            await _dbContext.Set<Test>().AddAsync(test);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Test test)
        {
            _dbContext.Set<Test>().Remove(test);
        }

        public async Task<IQueryable<Test>> GetAsync(TestFilter testFilter = null)
        {
            var query = _dbContext.Tests.AsQueryable();

            if (testFilter == null)
                return query;

            if (testFilter.Id != null)
                query = query.Where(test => test.Id == testFilter.Id);
            if (testFilter.Title != null)
                query = query.Where(test => test.Title == testFilter.Title);
            if (testFilter.Description != null)
                query = query.Where(test => test.Description == testFilter.Description);

            return query;
        }
    }
}

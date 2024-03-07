using Microsoft.EntityFrameworkCore;
using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class ApplicationUserTestRepository : IApplicationUserTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserTestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ApplicationUserTest applicationUserTest)
        {
            await _dbContext.Set<ApplicationUserTest>().AddAsync(applicationUserTest);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUserTest applicationUserTest)
        {
            _dbContext.Set<ApplicationUserTest>().Remove(applicationUserTest);
        }

        public async Task<IQueryable<ApplicationUserTest>> GetAsync(ApplicationUserTestFilter applicationUserTestFilter = null)
        {
            var query = _dbContext.ApplicationUserTests.AsQueryable();

            if (applicationUserTestFilter == null)
                return query;

            if (applicationUserTestFilter.Id != null)
                query = query.Where(applicationUserTest => applicationUserTest.Id == applicationUserTestFilter.Id);
            if (applicationUserTestFilter.ApplicationUserId != null)
                query = query.Where(applicationUserTest => applicationUserTest.ApplicationUserId == applicationUserTestFilter.ApplicationUserId);
            if (applicationUserTestFilter.TestId != null)
                query = query.Where(applicationUserTest => applicationUserTest.TestId == applicationUserTestFilter.TestId);
            if (applicationUserTestFilter.IsCompleted != null)
                query = query.Where(applicationUserTest => applicationUserTest.IsCompleted == applicationUserTestFilter.IsCompleted);

            return query;
        }
    }
}
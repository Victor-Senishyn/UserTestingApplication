using Microsoft.EntityFrameworkCore;
using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class UserTestResultRepository : IApplicationUserTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserTestResultRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserTestResult userTestResult)
        {
            await _dbContext.Set<UserTestResult>().AddAsync(userTestResult);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserTestResult userTestResult)
        {
            _dbContext.Set<UserTestResult>().Remove(userTestResult);
        }

        public async Task<IQueryable<UserTestResult>> GetAsync(UserTestResultFilter userTestResultFilter = null)
        {
            var query = _dbContext.UserTestResults.AsQueryable();

            if (userTestResultFilter == null)
                return query;

            if (userTestResultFilter.Id != null)
                query = query.Where(userTestResult => userTestResult.Id == userTestResultFilter.Id);
            if (userTestResultFilter.ApplicationUserId != null)
                query = query.Where(userTestResult => userTestResult.ApplicationUserId == userTestResultFilter.ApplicationUserId);
            if (userTestResultFilter.TestId != null)
                query = query.Where(userTestResult => userTestResult.TestId == userTestResultFilter.TestId);
            if (userTestResultFilter.IsCompleted != null)
                query = query.Where(userTestResult => userTestResult.IsCompleted == userTestResultFilter.IsCompleted);

            return query;
        }
    }
}
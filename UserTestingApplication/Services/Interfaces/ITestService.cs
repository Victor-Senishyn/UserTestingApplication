using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetAvailableTestsForUserAsync(string userId);
        Task<IEnumerable<CompletedTestDTO>> GetCompletedTestsForUserAsync(string userId);
        Task<Test> CreateTestsForUser(string userId);
    }
}

using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTestsForUserAsync(TestFilter testFilter);
        Task<Test> CreateTestsForUser(string userId);
    }
}

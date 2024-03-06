using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetAvailableTestsForUserAsync(ApplicationUserFilter applicationUserFilter);
        Task<IEnumerable<CompletedTestDTO>> GetCompletedTestsForUserAsync(ApplicationUserFilter applicationUserFilter);
    }
}

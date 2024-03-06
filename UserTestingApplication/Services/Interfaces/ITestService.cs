using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<Test>> GetAvailableTestsForUserAsync(ApplicationUserFilter applicationUserFilter);
        Task<IEnumerable<CompletedTest>> GetCompletedTestsForUserAsync(ApplicationUserFilter applicationUserFilter);
    }
}

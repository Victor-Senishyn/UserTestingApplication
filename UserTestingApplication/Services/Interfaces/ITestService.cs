using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTestsForUserAsync(ApplicationUserTestFilter applicationUserTestFilter);
        Task<Test> CreateTestsForUser(string userId);
        Task<IEnumerable<QuestionDTO>> GetQuestionsForTest(int testId);
    }
}

using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTestsForUserAsync(ApplicationUserTestFilter applicationUserTestFilter);
        Task<TestDTO> CreateTestsForUser(string userId);
        Task<ApplicationUserTestDTO> SubmitUserAnswers(UserAnswer userAnswer);
        Task<IEnumerable<QuestionDTO>> GetQuestionsForTest(int testId);
    }
}

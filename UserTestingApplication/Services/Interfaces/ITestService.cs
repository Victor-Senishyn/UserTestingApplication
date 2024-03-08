using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTestsForUserAsync(ApplicationUserTestFilter applicationUserTestFilter, CancellationToken cancellationToken = default);
        Task<TestDTO> CreateTestsForUser(string userId, CancellationToken cancellationToken = default);
        Task<ApplicationUserTestDTO> SubmitUserAnswers(UserAnswer userAnswer, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<QuestionDTO>> GetQuestionsForTest(int testId, CancellationToken cancellationToken = default);
    }
}

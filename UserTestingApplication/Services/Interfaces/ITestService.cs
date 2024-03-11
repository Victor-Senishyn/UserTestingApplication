using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTestsForUserAsync(UserTestResultFilter applicationUserTestFilter, CancellationToken cancellationToken = default);
        Task<UserTestResultDTO> SubmitUserAnswersAsync(UserAnswer userAnswer, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<QuestionDTO>> GetQuestionsForTestAsync(int testId, CancellationToken cancellationToken = default);
    }
}

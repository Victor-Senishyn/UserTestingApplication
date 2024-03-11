using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using UserTestingApplication.Exceptions;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Services;
using UserTestingApplication.Services.Interfaces;

namespace UserTestingApplication.Controllers
{
    [ApiController]
    [Route("api/tests")]
    [Authorize]
    public class TestsController : Controller
    {
        private ITestService _testService;

        public string UserId
        {
            get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        public TestsController(
            ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests(
            CancellationToken cancellationToken)
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new UserTestResultFilter { ApplicationUserId = UserId }, cancellationToken);

            return Ok(completedTests);
        }

        [HttpGet("/completed")]
        public async Task<IActionResult> GetCompletedTests(
            CancellationToken cancellationToken)
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new UserTestResultFilter { ApplicationUserId = UserId, IsCompleted = true }, cancellationToken);

            return Ok(completedTests);
        }

        [HttpGet("/available")]
        public async Task<IActionResult> GetAvailableTests(
            CancellationToken cancellationToken)
        {
            var availableTests = await _testService.GetTestsForUserAsync(
                new UserTestResultFilter { ApplicationUserId = UserId, IsCompleted = false }, cancellationToken);

            return Ok(availableTests);
        }

        [HttpGet("/questions/{testId}")]
        public async Task<IActionResult> GetQuestionsForTest(
            int testId,
            CancellationToken cancellationToken)
        {
            try
            {
                var questions = await _testService.GetQuestionsForTestAsync(testId, cancellationToken);
                return Ok(questions);
            }
            catch (TestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> SubmitUserAnswers(
            [FromBody] UserAnswer userAnswer,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _testService.SubmitUserAnswersAsync(userAnswer, UserId, cancellationToken);
                return Ok(result);
            }
            catch (DataValidationException ex) 
            when (ex is TestAlreadyPassedException || ex is DataValidationException)
            {
                return BadRequest(ex.Message);
            }
            catch (TestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

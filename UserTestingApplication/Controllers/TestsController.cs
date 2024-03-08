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
    [Route("/tests/")]
    [Authorize]
    public class TestsController : Controller
    {
        private ITestService _testService;
        private readonly string _userId;

        public TestsController(
            ITestService testService,
            IHttpContextAccessor httpContextAccessor)
        {
            _testService = testService;
            _userId = httpContextAccessor.HttpContext
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        [HttpGet("/tests/")]
        public async Task<IActionResult> GetTestsAsync(
            CancellationToken cancellationToken)
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new ApplicationUserTestFilter { ApplicationUserId = _userId }, cancellationToken);

            return Ok(completedTests);
        }

        [HttpGet("/tests/completed")]
        public async Task<IActionResult> GetCompletedTestsAsync(
            CancellationToken cancellationToken)
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new ApplicationUserTestFilter { ApplicationUserId = _userId, IsCompleted = true }, cancellationToken);

            return Ok(completedTests);
        }

        [HttpGet("/tests/available")]
        public async Task<IActionResult> GetAvailableTestsAsync(
            CancellationToken cancellationToken)
        {
            var availableTests = await _testService.GetTestsForUserAsync(
                new ApplicationUserTestFilter { ApplicationUserId = _userId, IsCompleted = false }, cancellationToken);

            return Ok(availableTests);
        }

        [HttpGet("{testId}/questions")]
        public async Task<IActionResult> GetQuestionsForTest(
            int testId,
            CancellationToken cancellationToken)
        {
            try
            {
                var questions = await _testService.GetQuestionsForTest(testId, cancellationToken);
                return Ok(questions);
            }
            catch (TestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUserAnswers(
            [FromBody] UserAnswer userAnswer,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _testService.SubmitUserAnswers(userAnswer, _userId, cancellationToken);
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


        [HttpPost("/tests/add")]
        public async Task<IActionResult> AddTest(
            CancellationToken cancellationToken)
        {
            var test = await _testService.CreateTestsForUser(_userId, cancellationToken);

            return Ok(test); 
        }
    }
}

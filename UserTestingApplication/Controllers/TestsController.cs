using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet("/tests/")]
        public async Task<IActionResult> GetTestsAsync()
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new TestFilter { ApplicationUserId = _userId });

            return Ok(completedTests);
        }

        [HttpGet("/tests/completed")]
        public async Task<IActionResult> GetCompletedTestsAsync()
        {
            var completedTests = await _testService.GetTestsForUserAsync(
                new TestFilter { ApplicationUserId = _userId, IsCompleted = true });

            return Ok(completedTests);
        }

        [HttpGet("/tests/available")]
        public async Task<IActionResult> GetAvailableTestsAsync()
        {
            var availableTests = await _testService.GetTestsForUserAsync(
                new TestFilter { ApplicationUserId = _userId, IsCompleted = false });

            return Ok(availableTests);
        }

        [HttpPost("/tests/add")]
        public async Task<IActionResult> AddTest()
        {
            var test = await _testService.CreateTestsForUser(_userId);

            return Ok(test); 
        }
    }
}

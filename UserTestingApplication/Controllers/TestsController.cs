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

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        //[HttpGet("/tests/completed")]
        //public async Task<IActionResult> GetCompletedTestsAsync()
        //{
        //    string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    var completedTests = await _testService.GetCompletedTestsForUserAsync(userId);

        //    return Ok(completedTests);
        //}

        [HttpGet("/tests/available")]
        public async Task<IActionResult> GetAvailableTestsAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var availableTests = await _testService.GetAvailableTestsForUserAsync(userId);

            return Ok(availableTests);
        }

        [HttpPost("/tests/add")]
        public async Task<IActionResult> AddTest()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var test = await _testService.CreateTestsForUser(userId);

            return Ok(test); 
        }
    }
}

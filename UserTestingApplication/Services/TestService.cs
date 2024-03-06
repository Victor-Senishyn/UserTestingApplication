using UserTestingApplication.Models;
using UserTestingApplication.Repositories;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;
using UserTestingApplication.Services.Interfaces;

namespace UserTestingApplication.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public TestService(
            ITestRepository testRepository, 
            IApplicationUserRepository applicationUserRepository) 
        {
            _testRepository = testRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<IEnumerable<Test>> GetAvailableTestsForUserAsync(
            ApplicationUserFilter applicationUserFilter)
        {
            var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            return user.Tests;
        }

        public async Task<IEnumerable<CompletedTest>> GetCompletedTestsForUserAsync(
            ApplicationUserFilter applicationUserFilter)
        {
            var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            return user.CompletedTests;
        }
    }
}

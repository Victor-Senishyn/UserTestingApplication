using AutoMapper;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using UserTestingApplication.DTOs;
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
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository, 
            IApplicationUserRepository applicationUserRepository,
            IMapper maper) 
        {
            _testRepository = testRepository;
            _applicationUserRepository = applicationUserRepository;
            _mapper = maper;
        }

        public async Task<IEnumerable<TestDTO>> GetAvailableTestsForUserAsync(
            ApplicationUserFilter applicationUserFilter)
        {
            var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            return _mapper.Map<IEnumerable<TestDTO>>(user.Tests);
        }

        public async Task<IEnumerable<CompletedTestDTO>> GetCompletedTestsForUserAsync(
            ApplicationUserFilter applicationUserFilter)
        {
            var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            return _mapper.Map<IEnumerable<CompletedTestDTO>>(user.CompletedTests);
        }
    }
}

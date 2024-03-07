using AutoMapper;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;
using UserTestingApplication.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UserTestingApplication.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ICompletedTestRepository _completedTestRepository;
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
            string userId)
        {
            //var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).SingleOrDefault();

            //if (user == null)
            //    throw new Exception("User not found");

            //var completedTestIds = user.CompletedTests.Select(ct => ct.TestId).ToList();

            //var availableTests = (await _testRepository.GetAsync())
            //    .Where(t => !completedTestIds.Contains(t.Id));

            //return _mapper.Map<IEnumerable<TestDTO>>(availableTests);

            var availableTests = await _testRepository.GetTestsAsync();

            return _mapper.Map <IEnumerable<TestDTO>>(
                availableTests.Where(t => t.ApplicationUserId == userId));

            //var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            //return _mapper.Map<IEnumerable<TestDTO>>(user.Tests);
        }

        public async Task<IEnumerable<CompletedTestDTO>> GetCompletedTestsForUserAsync(
            string userId)
        {
            var completedTests = await _completedTestRepository.GetAsync();
            return _mapper.Map<IEnumerable<CompletedTestDTO>>(
                completedTests.Where(t => t.ApplicationUserId == userId));
            //var user = (await _applicationUserRepository.GetAsync(applicationUserFilter)).FirstOrDefault();
            //return _mapper.Map<IEnumerable<CompletedTestDTO>>(user.CompletedTests);
        }


        ////
        public async Task<Test> CreateTestsForUser(string userId)
        {
            var test = new Test()
            {
                Title = "Test",
                Description = "Test",
                Questions = new[]
                {
                    new Question
                    {
                        Text = "Q1",
                        Answers = new[]
                        {
                            new Answer
                            {
                                Text = "A1",
                                IsCorrect = true
                            },
                            new Answer
                            {
                                Text = "A2",
                                IsCorrect = false
                            },
                            new Answer
                            {
                                Text = "A3",
                                IsCorrect = false
                            }
                        }
                    },
                    new Question
                    {
                        Text = "Q2",
                        Answers = new[]
                        {
                            new Answer
                            {
                                Text = "A1",
                                IsCorrect = true
                            },
                            new Answer
                            {
                                Text = "A2",
                                IsCorrect = false
                            },
                            new Answer
                            {
                                Text = "A3",
                                IsCorrect = false
                            }
                        }
                    }
                }
            };
            await _testRepository.AddAsync(test);

            var user = (await _applicationUserRepository.GetAsync(
                new ApplicationUserFilter
                {
                    Id = userId
                })).SingleOrDefault();

            if (user != null)
            {
                if (user.Tests == null)
                {
                    user.Tests = new List<Test>(); 
                }

                user.Tests.Add(test); 

                await _applicationUserRepository.CommitAsync(); 
            }

            await _testRepository.CommitAsync();
            return test;
        }
    }
}

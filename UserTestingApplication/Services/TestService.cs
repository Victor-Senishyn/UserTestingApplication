﻿using AutoMapper;
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
        private readonly IApplicationUserTestRepository _applicationUserTestRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository, 
            IApplicationUserRepository applicationUserRepository,
            IApplicationUserTestRepository applicationUserTestRepository,
            IMapper maper) 
        {
            _testRepository = testRepository;
            _applicationUserRepository = applicationUserRepository;
            _applicationUserTestRepository = applicationUserTestRepository;
            _mapper = maper;
        }

        public async Task<IEnumerable<TestDTO>> GetTestsForUserAsync(
            ApplicationUserTestFilter applicationUserTestFilter)
        {
            if (applicationUserTestFilter == null)
                return _mapper.Map<IEnumerable<TestDTO>>
                    (await _testRepository.GetAsync());

            var usersTests = (await _applicationUserTestRepository
                .GetAsync(applicationUserTestFilter))
                .Include(t => t.Tests)
                .Select(aut => aut.TestId)
                .ToList();

            var tests = await _testRepository.GetAsync();
            tests = tests.Where(t => usersTests.Contains(t.Id));

            return _mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsForTest(int testId)
        {
            var test = (await _testRepository
                .GetAsync(new TestFilter { Id = testId }))
                .Include(t => t.Questions)
                .SingleOrDefault();

            if (test == null)
                return null;

            return _mapper.Map<IEnumerable<QuestionDTO>>(test.Questions);
        }

        public async Task<ApplicationUserTestDTO> SubmitUserAnswers(
            UserAnswer userAnswer)
        {
            var test = (await _testRepository
                .GetAsync(new TestFilter { Id = userAnswer.TestId }))
                .Include(t => t.Questions)
                .SingleOrDefault();

            if (test == null || test.Questions.Count != userAnswer.QuestionIds.Count)
                throw new Exception("Wrong data.");

            int countOfQuestions = test.Questions.Count;
            int correctAnswers = 0;

            for (int i = 0; i < countOfQuestions; i++)
            {
                var question = test.Questions.ElementAt(i);
                var userSelectedAnswerId = userAnswer.SelectedAnswerIds.ElementAt(i);

                if (question.Answers.Any(a => a.Id == userSelectedAnswerId && a.IsCorrect))
                    correctAnswers++;
            }

            int score = (int)(((double)correctAnswers / countOfQuestions) * 100);

            return new ApplicationUserTestDTO
            {
                IsCompleted = true,
                Score = score,
                ApplicationUserId = userAnswer.UserId,
                TestId = test.Id
            };
        }


        public async Task<TestDTO> CreateTestsForUser(string userId)
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
            await _testRepository.CommitAsync();

            var applicationUserTest = new ApplicationUserTest { ApplicationUserId = userId, TestId = test.Id, IsCompleted = true };
            await _applicationUserTestRepository.AddAsync(applicationUserTest);

            await _testRepository.CommitAsync();
            return _mapper.Map<TestDTO>(test);
        }
    }
}

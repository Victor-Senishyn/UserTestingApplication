using AutoMapper;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserTestingApplication.DTOs;
using UserTestingApplication.Exceptions;
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
        private readonly IApplicationUserTestRepository _userTestResultRepository;
        private readonly IMapper _mapper;

        public TestService(
            ITestRepository testRepository, 
            IApplicationUserTestRepository applicationUserTestRepository,
            IMapper maper) 
        {
            _testRepository = testRepository;
            _userTestResultRepository = applicationUserTestRepository;
            _mapper = maper;
        }

        public async Task<IEnumerable<TestDTO>> GetTestsForUserAsync(
            UserTestResultFilter applicationUserTestFilter,
            CancellationToken cancellationToken = default)
        {
            if (applicationUserTestFilter == null)
                return _mapper.Map<IEnumerable<TestDTO>>
                    (await _testRepository.GetAsync());

            var usersTests = (await _userTestResultRepository
                .GetAsync(applicationUserTestFilter))
                .Include(t => t.Tests)
                .Select(aut => aut.TestId)
                .ToList();

            var tests = await _testRepository.GetAsync();
            tests = tests.Where(t => usersTests.Contains(t.Id));

            return _mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsForTestAsync(
            int testId,
            CancellationToken cancellationToken = default)
        {
            var test = (await _testRepository
                .GetAsync(new TestFilter { Id = testId }))
                .Include(t => t.Questions)
                .SingleOrDefault();

            if (test == null)
                throw new TestNotFoundException("Test not found.");

            return _mapper.Map<IEnumerable<QuestionDTO>>(test.Questions);
        }

        public async Task<UserTestResultDTO> SubmitUserAnswersAsync(
            UserAnswer userAnswer, 
            string userId,
            CancellationToken cancellationToken = default)
        {
            var userTestResult = (await _userTestResultRepository.GetAsync(
                new UserTestResultFilter { TestId = userAnswer.TestId, ApplicationUserId = userId }))
                .FirstOrDefault();

            if (userTestResult == null)
                throw new DataValidationException("Wrong data.");

            if (userTestResult.IsCompleted)
                throw new TestAlreadyPassedException("The test has already been passed");

            var test = (await _testRepository
                .GetAsync(new TestFilter { Id = userAnswer.TestId }))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .SingleOrDefault();

            if (test == null || test.Questions.Count != userAnswer.QuestionIds.Count)
                throw new TestNotFoundException("Test not found.");

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

            userTestResult.Score = score;
            userTestResult.IsCompleted = true;

            await _userTestResultRepository.CommitAsync();

            return _mapper.Map<UserTestResultDTO>(userTestResult);
        }

        public async Task<TestDTO> CreateTestsForUserAsync(
            string userId,
            CancellationToken cancellationToken = default)
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

            var userTestResult = new UserTestResult { ApplicationUserId = userId, TestId = test.Id, IsCompleted = false };
            await _userTestResultRepository.AddAsync(userTestResult);

            await _testRepository.CommitAsync();
            return _mapper.Map<TestDTO>(test);
        }
    }
}

using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UserTestingApplication.DTOs;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Services;
using Xunit;
using UserTestingApplication.Repositories.Inerfaces;
using UserTestingApplication.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using UserTestingApplication.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Routing;

namespace UserTestingApplication.Tests.Services
{
    public class TestServiceTests
    {
        private TestService _testService;
        private ITestRepository _fakeTestRepository;
        private IApplicationUserTestRepository _fakeUserTestResultRepository;
        private IMapper _fakeMapper;

        public TestServiceTests()
        {
            _fakeTestRepository = A.Fake<ITestRepository>();
            _fakeUserTestResultRepository = A.Fake<IApplicationUserTestRepository>();
            _fakeMapper = A.Fake<IMapper>();

            _testService = new TestService(_fakeTestRepository, _fakeUserTestResultRepository, _fakeMapper);
        }

        [Fact]
        public async Task GetTestsForUserAsync_WithNullFilter_ReturnsAllTests()
        {
            // Arrange

            A.CallTo(() => _fakeTestRepository.GetAsync(null))
               .Returns(Task.FromResult<IQueryable<Test>>(new List<Test> {  }.AsQueryable()));

            // Act
            var result = await _testService.GetTestsForUserAsync(null ,CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.All(result, question => Assert.NotNull(question));
        }

        [Fact]
        public async Task GetQuestionsForTestAsync_WithValidTestId_ReturnsQuestions()
        {
            // Arrange
            int testId = 1;
            var cancellationToken = CancellationToken.None;

            var fakeTest = new Test
            {
                Id = testId,
                Questions = new List<Question>
                {
                    new Question { Id = 1, Text = "Question 1", Answers = new List<Answer> { new Answer { Id = 1, Text = "Answer 1" } } },
                    new Question { Id = 2, Text = "Question 2", Answers = new List<Answer> { new Answer { Id = 2, Text = "Answer 2" } } }
                }
            };

            A.CallTo(() => _fakeTestRepository.GetAsync(A<TestFilter>.That.Matches(filter => filter.Id == testId)))
               .Returns(Task.FromResult<IQueryable<Test>>(new List<Test> { fakeTest }.AsQueryable()));

            // Act
            var result = await _testService.GetQuestionsForTestAsync(testId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, question => Assert.NotNull(question));
        }

        [Fact]
        public async Task SubmitUserAnswersAsync_WithValidData_ReturnsUserTestResultDTO()
        {
            // Arrange
            var userAnswer = new UserAnswer
            {
                TestId = 1,
                QuestionIds = new List<int> { 1, 2 },
                SelectedAnswerIds = new List<int> { 1, 2 }
            };
            string userId = "testUserId";
            var cancellationToken = CancellationToken.None;

            var userTestResult = new UserTestResult
            {
                TestId = userAnswer.TestId,
                ApplicationUserId = userId,
                IsCompleted = false
            };

            var fakeUserTestResultRepository = A.Fake<IApplicationUserTestRepository>();
            A.CallTo(() => fakeUserTestResultRepository.GetAsync(A<UserTestResultFilter>._))
                .Returns(Task.FromResult(new List<UserTestResult> { userTestResult }.AsQueryable()));

            var fakeTestRepository = A.Fake<ITestRepository>();
            A.CallTo(() => fakeTestRepository.GetAsync(A<TestFilter>._))
                .Returns(Task.FromResult<IQueryable<Test>>(new List<Test>
                {
                    new Test
                    {
                        Id = userAnswer.TestId,
                        Questions = new List<Question>
                        {
                            new Question { Id = 1, Answers = new List<Answer> { new Answer { Id = 1, IsCorrect = true } } },
                            new Question { Id = 2, Answers = new List<Answer> { new Answer { Id = 2, IsCorrect = true } } }
                        }
                    }
                }.AsQueryable()));

            var testService = new TestService(fakeTestRepository, fakeUserTestResultRepository, A.Fake<IMapper>());

            // Act
            var result = await testService.SubmitUserAnswersAsync(userAnswer, userId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Score);
        }

    }
}

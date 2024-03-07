using System;
using System.Xml.Linq;
using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnswerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Answer answer)
        {
            await _dbContext.Set<Answer>().AddAsync(answer);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Answer answer)
        {
            _dbContext.Set<Answer>().Remove(answer);
        }

        public async Task<IQueryable<Answer>> GetAsync(AnswerFilter answerFilter = null)
        {
            var query = _dbContext.Answers.AsQueryable();

            if (answerFilter.Id != null)
                query = query.Where(answer => answer.Id == answerFilter.Id);
            if (answerFilter.IsCorrect != null)
                query = query.Where(answer => answer.IsCorrect == answerFilter.IsCorrect);
            if (answerFilter.Text != null)
                query = query.Where(answer => answer.Text == answerFilter.Text);

            return query;
        }
    }
}

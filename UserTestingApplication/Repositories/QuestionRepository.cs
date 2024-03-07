using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Question question)
        {
            await _dbContext.Set<Question>().AddAsync(question);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Question question)
        {
            _dbContext.Set<Question>().Remove(question);
        }

        public async Task<IQueryable<Question>> GetAsync(QuestionFilter questionFilter = null)
        {
            var query = _dbContext.Questions.AsQueryable();
            
            if(questionFilter == null)
                return query;

            if (questionFilter.Id != null)
                query = query.Where(question => question.Id == questionFilter.Id);
            if (questionFilter.Text != null)
                query = query.Where(question => question.Text == questionFilter.Text);

            return query;
        }
    }
}

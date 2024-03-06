using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface IQuestionRepository
    {
        Task<IQueryable<Question>> GetAsync(QuestionFilter questionFilter);
        Task AddAsync(Question question);
        Task CommitAsync();
        Task DeleteAsync(Question question);
    }
}

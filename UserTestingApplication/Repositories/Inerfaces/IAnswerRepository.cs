using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;

namespace UserTestingApplication.Repositories.Inerfaces
{
    public interface IAnswerRepository
    {
        Task<IQueryable<Answer>> GetAsync(AnswerFilter answerFilter = null);
        Task AddAsync(Answer answer);
        Task CommitAsync();
        Task DeleteAsync(Answer answer);
    }
}

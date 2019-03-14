using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
    }

    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IQuestionGroupRepository : IRepository<QuestionGroup>
    {
    }

    public class QuestionGroupRepository : RepositoryBase<QuestionGroup>, IQuestionGroupRepository
    {
        public QuestionGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

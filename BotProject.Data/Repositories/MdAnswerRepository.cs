using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IMdAnswerRepository : IRepository<MdAnswer>
    {
    }

    public class MdAnswerRepository : RepositoryBase<MdAnswer>, IMdAnswerRepository
    {
        public MdAnswerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
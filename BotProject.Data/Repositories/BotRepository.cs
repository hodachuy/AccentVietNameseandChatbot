using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IBotRepository : IRepository<Bot>
    {
    }

    public class BotRepository : RepositoryBase<Bot>, IBotRepository
    {
        public BotRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

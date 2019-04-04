using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IMdAreaRepository : IRepository<MdArea>
    {
    }

    public class MdAreaRepository : RepositoryBase<MdArea>, IMdAreaRepository
    {
        public MdAreaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
    }

    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
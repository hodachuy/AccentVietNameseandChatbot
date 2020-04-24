using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IGroupChannelRepository : IRepository<GroupChannel>
    {
    }

    public class GroupChannelRepository : RepositoryBase<GroupChannel>, IGroupChannelRepository
    {
        public GroupChannelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

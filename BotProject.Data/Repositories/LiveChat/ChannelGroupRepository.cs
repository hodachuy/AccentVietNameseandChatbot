using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IChannelGroupRepository : IRepository<ChannelGroup>
    {
    }

    public class ChannelGroupRepository : RepositoryBase<ChannelGroup>, IChannelGroupRepository
    {
        public ChannelGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

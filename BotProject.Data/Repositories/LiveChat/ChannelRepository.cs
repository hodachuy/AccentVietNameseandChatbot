using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IChannelRepository : IRepository<Channel>
    {

    }
    public class ChannelRepository : RepositoryBase<Channel>, IChannelRepository
    {
        public ChannelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

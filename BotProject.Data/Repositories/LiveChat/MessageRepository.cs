using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{

    public interface IMessageRepository : IRepository<Message>
    {
    }

    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

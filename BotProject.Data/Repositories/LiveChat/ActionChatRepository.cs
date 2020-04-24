using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IActionChatRepository : IRepository<ActionChat>
    {
    }

    public class ActionChatRepository : RepositoryBase<ActionChat>, IActionChatRepository
    {
        public ActionChatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

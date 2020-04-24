using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IChatWidgetLanguageRepository : IRepository<ChatWidgetLanguage>
    {

    }
    public class ChatWidgetLanguageRepository : RepositoryBase<ChatWidgetLanguage>, IChatWidgetLanguageRepository
    {
        public ChatWidgetLanguageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

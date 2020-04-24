using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IChatWidgetCustomizationRepository : IRepository<ChatWidgetCustomization>
    {

    }
    public class ChatWidgetCustomizationRepository : RepositoryBase<ChatWidgetCustomization>, IChatWidgetCustomizationRepository
    {
        public ChatWidgetCustomizationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

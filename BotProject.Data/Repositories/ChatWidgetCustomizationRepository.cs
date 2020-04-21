using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

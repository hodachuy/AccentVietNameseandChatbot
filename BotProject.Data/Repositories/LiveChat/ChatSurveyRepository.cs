using BotProject.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories.LiveChat
{
    public interface IChatSurveyRepository : IRepository<ChatSurveyRepository>
    {

    }
    public class ChatSurveyRepository : RepositoryBase<ChatSurveyRepository>, IChatSurveyRepository
    {
        public ChatSurveyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

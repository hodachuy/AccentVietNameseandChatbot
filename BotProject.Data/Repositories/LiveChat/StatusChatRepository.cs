using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IStatusChatRepository : IRepository<StatusChat>
    {
    }

    public class StatusChatRepository : RepositoryBase<StatusChat>, IStatusChatRepository
    {
        public StatusChatRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

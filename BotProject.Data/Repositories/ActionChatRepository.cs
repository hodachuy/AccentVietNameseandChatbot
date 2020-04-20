using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

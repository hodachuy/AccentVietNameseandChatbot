using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

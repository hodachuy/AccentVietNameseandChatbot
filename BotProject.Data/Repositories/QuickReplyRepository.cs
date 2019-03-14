using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IQuickReplyRepository : IRepository<QuickReply>
    {
    }

    public class QuickReplyRepository : RepositoryBase<QuickReply>, IQuickReplyRepository
    {
        public QuickReplyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

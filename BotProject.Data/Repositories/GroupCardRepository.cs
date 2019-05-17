using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IGroupCardRepository : IRepository<GroupCard>
    {
    }

    public class GroupCardRepository : RepositoryBase<GroupCard>, IGroupCardRepository
    {
        public GroupCardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

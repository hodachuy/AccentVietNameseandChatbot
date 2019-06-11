using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IModuleFollowCardRepository : IRepository<ModuleFollowCard>
    {
    }

    public class ModuleFollowCardRepository : RepositoryBase<ModuleFollowCard>, IModuleFollowCardRepository
    {
        public ModuleFollowCardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

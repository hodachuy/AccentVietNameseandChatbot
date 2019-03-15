using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IAIMLRepository : IRepository<AIML>
    {
    }

    public class AIMLRepository : RepositoryBase<AIML>, IAIMLRepository
    {
        public AIMLRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

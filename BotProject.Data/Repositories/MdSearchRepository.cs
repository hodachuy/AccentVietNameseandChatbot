using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdSearchRepository : IRepository<MdSearch>
    {
    }

    public class MdSearchRepository : RepositoryBase<MdSearch>, IMdSearchRepository
    {
        public MdSearchRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

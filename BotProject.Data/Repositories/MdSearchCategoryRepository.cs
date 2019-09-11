using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdSearchCategoryRepository : IRepository<MdSearchCategory>
    {
    }

    public class MdSearchCategoryRepository : RepositoryBase<MdSearchCategory>, IMdSearchCategoryRepository
    {
        public MdSearchCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

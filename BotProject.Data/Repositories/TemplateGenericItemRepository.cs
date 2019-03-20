using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface ITemplateGenericItemRepository : IRepository<TemplateGenericItem>
    {
    }

    public class TemplateGenericItemRepository : RepositoryBase<TemplateGenericItem>, ITemplateGenericItemRepository
    {
        public TemplateGenericItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

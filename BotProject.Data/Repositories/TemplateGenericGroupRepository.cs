using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface ITemplateGenericGroupRepository : IRepository<TemplateGenericGroup>
    {
    }

    public class TemplateGenericGroupRepository : RepositoryBase<TemplateGenericGroup>, ITemplateGenericGroupRepository
    {
        public TemplateGenericGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

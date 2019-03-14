using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface ITemplateGenericRepository : IRepository<TemplateGeneric>
    {
    }

    public class TemplateGenericRepository : RepositoryBase<TemplateGeneric>, ITemplateGenericRepository
    {
        public TemplateGenericRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

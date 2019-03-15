using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface ITemplateTextRepository : IRepository<TemplateText>
    {
    }

    public class TemplateTextRepository : RepositoryBase<TemplateText>, ITemplateTextRepository
    {
        public TemplateTextRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

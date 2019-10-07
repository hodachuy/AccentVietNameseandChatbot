using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IApplicationThirdPartyRepository : IRepository<ApplicationThirdParty>
    {

    }
    public class ApplicationThirdPartyRepository : RepositoryBase<ApplicationThirdParty>, IApplicationThirdPartyRepository
    {
        public ApplicationThirdPartyRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

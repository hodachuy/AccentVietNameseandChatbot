using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IApplicationPlatformUserRepository : IRepository<ApplicationPlatformUser>
    {
    }

    public class ApplicationPlatformUserRepository : RepositoryBase<ApplicationPlatformUser>, IApplicationPlatformUserRepository
    {
        public ApplicationPlatformUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

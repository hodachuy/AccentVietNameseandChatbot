using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IAttributePlatformUserRepository : IRepository<AttributePlatformUser>
    {
    }

    public class AttributePlatformUserRepository : RepositoryBase<AttributePlatformUser>, IAttributePlatformUserRepository
    {
        public AttributePlatformUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

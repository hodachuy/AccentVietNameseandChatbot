using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IUserTelephoneRepository : IRepository<UserTelePhone>
    {
    }

    public class UserTelephoneRepository : RepositoryBase<UserTelePhone>, IUserTelephoneRepository
    {
        public UserTelephoneRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

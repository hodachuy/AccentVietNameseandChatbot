using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IApplicationFacebookUserRepository : IRepository<ApplicationFacebookUser>
    {
    }

    public class ApplicationFacebookUserRepository : RepositoryBase<ApplicationFacebookUser>, IApplicationFacebookUserRepository
    {
        public ApplicationFacebookUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

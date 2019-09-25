using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IApplicationZaloUserRepository : IRepository<ApplicationZaloUser>
    {
    }

    public class ApplicationZaloUserRepository : RepositoryBase<ApplicationZaloUser>, IApplicationZaloUserRepository
    {
        public ApplicationZaloUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

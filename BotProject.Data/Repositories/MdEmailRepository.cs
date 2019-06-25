using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdEmailRepository : IRepository<MdEmail>
    {
    }

    public class MdEmailRepository : RepositoryBase<MdEmail>, IMdEmailRepository
    {
        public MdEmailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

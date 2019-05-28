using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdPhoneRepository : IRepository<MdPhone>
    {
    }

    public class MdPhoneRepository : RepositoryBase<MdPhone>, IMdPhoneRepository
    {
        public MdPhoneRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

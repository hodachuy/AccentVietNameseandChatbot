using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdAgeRepository : IRepository<MdAge>
    {
    }

    public class MdAgeRepository : RepositoryBase<MdAge>, IMdAgeRepository
    {
        public MdAgeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

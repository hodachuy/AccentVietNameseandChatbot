using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdQuestionRepository : IRepository<MdQuestion>
    {
    }

    public class MdQuestionRepository : RepositoryBase<MdQuestion>, IMdQuestionRepository
    {
        public MdQuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

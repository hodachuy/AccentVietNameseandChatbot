using BotProject.Data.Infrastructure;
using BotProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IHistoryRepository : IRepository<History>
    {
    }

    public class HistoryRepository : RepositoryBase<History>, IHistoryRepository
    {
        public HistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

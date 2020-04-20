using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IThreadRepository : IRepository<Thread>
    {
    }

    public class ThreadRepository : RepositoryBase<Thread>, IThreadRepository
    {
        public ThreadRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IThreadParticipantRepository : IRepository<ThreadParticipant>
    {
    }

    public class ThreadParticipantRepository : RepositoryBase<ThreadParticipant>, IThreadParticipantRepository
    {
        public ThreadParticipantRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

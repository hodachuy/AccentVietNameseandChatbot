using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

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

using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

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

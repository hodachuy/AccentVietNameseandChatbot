using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IFileCardRepository : IRepository<FileCard>
    {
    }

    public class FileCardRepository : RepositoryBase<FileCard>, IFileCardRepository
    {
        public FileCardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
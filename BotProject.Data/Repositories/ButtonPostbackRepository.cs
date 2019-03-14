using BotProject.Data.Infrastructure;
using BotProject.Model.Models;

namespace BotProject.Data.Repositories
{
    public interface IButtonPostbackRepository : IRepository<ButtonPostback>
    {
    }

    public class ButtonPostbackRepository : RepositoryBase<ButtonPostback>, IButtonPostbackRepository
    {
        public ButtonPostbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

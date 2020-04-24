using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

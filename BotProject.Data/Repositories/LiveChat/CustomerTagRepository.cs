using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories.LiveChat
{
    public interface ICustomerTagRepository : IRepository<CustomerTag>
    {
    }

    public class CustomerTagRepository : RepositoryBase<CustomerTag>, ICustomerTagRepository
    {
        public CustomerTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

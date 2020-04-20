using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IChannelRepository : IRepository<Channel>
    {

    }
    public class ChannelRepository : RepositoryBase<Channel>, IChannelRepository
    {
        public ChannelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

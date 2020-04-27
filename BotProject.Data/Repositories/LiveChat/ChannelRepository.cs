using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using BotProject.Common.ViewModels.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IChannelRepository : IRepository<Channel>
    {
        IEnumerable<SP_Channel> sp_GetListChannel(long groupChannelID);
    }
    public class ChannelRepository : RepositoryBase<Channel>, IChannelRepository
    {
        public ChannelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<SP_Channel> sp_GetListChannel(long groupChannelID)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@GroupChanelID",groupChannelID)
            };
            var query = DbContext.Database.SqlQuery<SP_Channel>(
                "sp_LiveChat_GetListChannel @GroupChanelID",
                parameters);
            return query;
        }
    }
}

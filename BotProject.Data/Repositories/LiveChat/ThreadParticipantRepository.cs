using BotProject.Common.ViewModels.LiveChat;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace BotProject.Data.Repositories
{
    public interface IThreadParticipantRepository : IRepository<ThreadParticipant>
    {
        IEnumerable<SP_CustomerJoin> GetCustomerJoinChatChannel(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null);

    }

    public class ThreadParticipantRepository : RepositoryBase<ThreadParticipant>, IThreadParticipantRepository
    {
        public ThreadParticipantRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<SP_CustomerJoin> GetCustomerJoinChatChannel(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = default(long?))
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<SP_CustomerJoin>(
                "sp_LiveChat_GetCustomerJoinChatChannel @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
    }
}

using BotProject.Common.ViewModels.LiveChat;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        IEnumerable<SP_ConversationMessage> GetListConversationMessage(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null);
    }
    public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
    {
        public ConversationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<SP_ConversationMessage> GetListConversationMessage(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<SP_ConversationMessage>(
                "sp_LiveChat_GetListConversationMessage @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
    }
}

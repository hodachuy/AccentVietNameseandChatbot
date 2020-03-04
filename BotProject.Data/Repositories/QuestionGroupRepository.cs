using BotProject.Common.ViewModels;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BotProject.Data.Repositories
{
    public interface IQuestionGroupRepository : IRepository<QuestionGroup>
    {
        IEnumerable<StoreProcQuesGroupViewModel> GetListQuesGroup(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null);
        int CheckExitsQuestionScriptByBotID(string contentText, int botID, int quesGroupID);
    }

    public class QuestionGroupRepository : RepositoryBase<QuestionGroup>, IQuestionGroupRepository
    {
        public QuestionGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<StoreProcQuesGroupViewModel> GetListQuesGroup(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<StoreProcQuesGroupViewModel>(
                "sp_GetListQuesGroup @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
        public int CheckExitsQuestionScriptByBotID(string contentText, int botID, int quesGroupID)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@ContentText",(String.IsNullOrEmpty(contentText) == true ? "": contentText)),
                new SqlParameter("@BotID",botID),
                new SqlParameter("@QuestionGroupID",quesGroupID),
            };

            var query = DbContext.Database.SqlQuery<int>(
                "sp_CheckExitsQuestionScriptByBotID @ContentText,@BotID,@QuestionGroupID",
                parameters).FirstOrDefault();
            return query;
        }
    }
}

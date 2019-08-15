using BotProject.Common.ViewModels;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace BotProject.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        QuesTargetViewModel GetQuesByTarget(string target, int botId);
    }

    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public QuesTargetViewModel GetQuesByTarget(string target, int botId)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Target",(String.IsNullOrEmpty(target) == true ? "": target)),
                new SqlParameter("@BotID",botId),
            };
            return DbContext.Database.SqlQuery<QuesTargetViewModel>("sp_GetQuesByTarget @Target,@BotID", parameters).FirstOrDefault();
        }
    }
}

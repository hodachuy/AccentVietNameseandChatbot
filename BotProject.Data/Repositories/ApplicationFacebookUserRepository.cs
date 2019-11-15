using BotProject.Common.ViewModels;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IApplicationFacebookUserRepository : IRepository<ApplicationFacebookUser>
    {
        int CheckDuplicateRequestWithTimeStamp(string timeStamp, string userId);
    }

    public class ApplicationFacebookUserRepository : RepositoryBase<ApplicationFacebookUser>, IApplicationFacebookUserRepository
    {
        public ApplicationFacebookUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int CheckDuplicateRequestWithTimeStamp(string timeStamp, string userId)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@FbTimeStamp",(String.IsNullOrEmpty(timeStamp) == true ? "": timeStamp)),
                new SqlParameter("@FbSenderId",(String.IsNullOrEmpty(userId) == true ? "": userId))
            };
            
            var query = DbContext.Database.SqlQuery<int>(
                "sp_FB_CheckRequestDuplicateWithTimeStamp @FbTimeStamp,@FbSenderId",
                parameters).FirstOrDefault();
            return query;
        }
    }
}

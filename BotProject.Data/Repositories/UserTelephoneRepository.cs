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
    public interface IUserTelephoneRepository : IRepository<UserTelePhone>
    {
        IEnumerable<StoreProcUserTelephoneByVoucherViewModel> GetUserTelephoneByVoucher(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null);

    }

    public class UserTelephoneRepository : RepositoryBase<UserTelePhone>, IUserTelephoneRepository
    {
        public UserTelephoneRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<StoreProcUserTelephoneByVoucherViewModel> GetUserTelephoneByVoucher(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = default(long?))
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<StoreProcUserTelephoneByVoucherViewModel>(
                "sp_GetUserTelephoneByVoucher @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
    }
}

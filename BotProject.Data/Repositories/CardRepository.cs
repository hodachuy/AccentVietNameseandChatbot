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
    public interface ICardRepository : IRepository<Card>
    {
        StoreProcCardViewModel GetCardByPattern(string pattern);
        IEnumerable<SPCardViewModel> GetListCard(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = default(long?));
    }

    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public StoreProcCardViewModel GetCardByPattern(string pattern)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@PatternText", (String.IsNullOrEmpty(pattern) == true ? "": pattern))
            };
            var query = DbContext.Database.SqlQuery<StoreProcCardViewModel>(
                "sp_GetCardByPattern @PatternText",
                parameters).SingleOrDefault();
            return query;
        }

        public IEnumerable<SPCardViewModel> GetListCard(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = default(long?))
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<SPCardViewModel>(
                "sp_GetListCard @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
    }
}

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
    }
}

using BotProject.Common.ViewModels;
using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace BotProject.Data.Repositories
{
    public interface IBotRepository : IRepository<Bot>
    {
        IEnumerable<StoreProcBotViewModel> GetListBotDashboard(string userId);
    }

    public class BotRepository : RepositoryBase<Bot>, IBotRepository
    {
        public BotRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<StoreProcBotViewModel> GetListBotDashboard(string userId)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@UserId", userId),
            };
            var query = DbContext.Database.SqlQuery<StoreProcBotViewModel>(
                "sp_GetListBot @UserId", parameters);
            return query;
        }
    }
}

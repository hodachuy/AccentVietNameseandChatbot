﻿using BotProject.Common.ViewModels;
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
    public interface IHistoryRepository : IRepository<History>
    {
        IEnumerable<StoreProcHistoryViewModel> GetListHistory(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null);
    }

    public class HistoryRepository : RepositoryBase<History>, IHistoryRepository
    {
        public HistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<StoreProcHistoryViewModel> GetListHistory(string Filter, string Sort, int PageNumber, int PageSize, long? SelectedID = null)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@Filter",(String.IsNullOrEmpty(Filter) == true ? "": Filter)),
                new SqlParameter("@Sort",(String.IsNullOrEmpty(Sort) == true ? "": Sort)),
                new SqlParameter("@PageNumber",PageNumber),
                new SqlParameter("@PageSize",PageSize),
                new SqlParameter("@SelectedID",(object)SelectedID??DBNull.Value),
            };
            var query = DbContext.Database.SqlQuery<StoreProcHistoryViewModel>(
                "sp_GetListHistory @Filter,@Sort,@PageNumber,@PageSize,@SelectedID",
                parameters);
            return query;
        }
    }
}

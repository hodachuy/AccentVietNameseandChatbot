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
    public interface IModuleCategoryRepository : IRepository<ModuleCategory>
    {
    }

    public class ModuleCategoryRepository : RepositoryBase<ModuleCategory>, IModuleCategoryRepository
    {
        public ModuleCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

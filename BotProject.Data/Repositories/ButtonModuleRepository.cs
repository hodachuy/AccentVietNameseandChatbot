using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IButtonModuleRepository : IRepository<ButtonModule>
    {
    }

    public class ButtonModuleRepository : RepositoryBase<ButtonModule>, IButtonModuleRepository
    {
        public ButtonModuleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

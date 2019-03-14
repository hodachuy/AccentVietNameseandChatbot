using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IButtonLinkRepository : IRepository<ButtonLink>
    {
    }

    public class ButtonLinkRepository : RepositoryBase<ButtonLink>, IButtonLinkRepository
    {
        public ButtonLinkRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

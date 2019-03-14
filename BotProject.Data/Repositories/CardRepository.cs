using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
    }

    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

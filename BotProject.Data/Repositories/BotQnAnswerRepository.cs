using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IBotQnAnswerRepository : IRepository<BotQnAnswer>
	{
	}

	public class BotQnAnswerRepository : RepositoryBase<BotQnAnswer>, IBotQnAnswerRepository
	{
		public BotQnAnswerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

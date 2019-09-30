using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IMdEngineerNameRepository : IRepository<MdEngineerName>
	{
	}

	public class MdEngineerNameRepository : RepositoryBase<MdEngineerName>, IMdEngineerNameRepository
	{
		public MdEngineerNameRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

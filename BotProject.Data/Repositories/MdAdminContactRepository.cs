using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IMdAdminContactRepository : IRepository<MdAdminContact>
	{
	}

	public class MdAdminContactRepository : RepositoryBase<MdAdminContact>, IMdAdminContactRepository
	{
		public MdAdminContactRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

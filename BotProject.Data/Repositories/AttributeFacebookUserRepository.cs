using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IAttributeFacebookUserRepository : IRepository<AttributeFacebookUser>
	{
	}

	public class AttributeFacebookUserRepository : RepositoryBase<AttributeFacebookUser>, IAttributeFacebookUserRepository
	{
		public AttributeFacebookUserRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

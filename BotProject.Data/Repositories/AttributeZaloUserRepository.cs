using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IAttributeZaloUserRepository : IRepository<AttributeZaloUser>
	{
	}

	public class AttributeZaloUserRepository : RepositoryBase<AttributeZaloUser>, IAttributeZaloUserRepository
	{
		public AttributeZaloUserRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

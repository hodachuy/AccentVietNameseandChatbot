using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IAttributeSystemRepository : IRepository<AttributeSystem>
	{
	}

	public class AttributeSystemRepository : RepositoryBase<AttributeSystem>, IAttributeSystemRepository
	{
		public AttributeSystemRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

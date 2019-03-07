using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface INotifyRepository : IRepository<Notify>
	{
	}

	public class NotifyRepository : RepositoryBase<Notify>, INotifyRepository
	{
		public NotifyRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

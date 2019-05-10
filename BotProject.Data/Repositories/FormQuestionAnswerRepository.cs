using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
	public interface IFormQuestionAnswerRepository : IRepository<FormQuestionAnswer>
	{
	}

	public class FormQuestionAnswerRepository : RepositoryBase<FormQuestionAnswer>, IFormQuestionAnswerRepository
    {
		public FormQuestionAnswerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}

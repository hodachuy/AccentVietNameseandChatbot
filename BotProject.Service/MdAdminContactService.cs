using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service
{
	class MdAdminContactService
	{
	}
	public interface IMdAdminContactService
	{
		MdAdminContact GetByBotID(int botId);
		MdAdminContact Create(MdAdminContact module);
		void Update(MdAdminContact module);
		void Save();
	}
	public class MdAdminContactServiceService : IMdAdminContactService
	{
		IMdAdminContactRepository _mdAdminContactRepository;
		IUnitOfWork _unitOfWork;
		public MdAdminContactServiceService(IUnitOfWork unitOfWork, IMdAdminContactRepository mdAdminContactRepository)
		{
			_unitOfWork = unitOfWork;
			_mdAdminContactRepository = mdAdminContactRepository;
		}
		public MdAdminContact Create(MdAdminContact module)
		{
			return _mdAdminContactRepository.Add(module);
		}

		public MdAdminContact GetByBotID(int botId)
		{
			return _mdAdminContactRepository.GetSingleByCondition(x => x.BotID == botId);
		}
		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(MdAdminContact module)
		{
			_mdAdminContactRepository.Update(module);
		}
	}
}

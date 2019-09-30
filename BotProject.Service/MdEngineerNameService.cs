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
	public interface IMdEngineerNameService
	{
		MdEngineerName GetByBotID(int botId);
		MdEngineerName Create(MdEngineerName module);
		void Update(MdEngineerName module);
		void Save();
	}
	public class MdEngineerNameService : IMdEngineerNameService
	{
		IMdEngineerNameRepository _mdEngineerNameRepository;
		IUnitOfWork _unitOfWork;
		public MdEngineerNameService(IUnitOfWork unitOfWork, IMdEngineerNameRepository mdEngineerNameRepository)
		{
			_unitOfWork = unitOfWork;
			_mdEngineerNameRepository = mdEngineerNameRepository;
		}
		public MdEngineerName Create(MdEngineerName module)
		{
			return _mdEngineerNameRepository.Add(module);
		}

		public MdEngineerName GetByBotID(int botId)
		{
			return _mdEngineerNameRepository.GetSingleByCondition(x => x.BotID == botId);
		}
		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(MdEngineerName module)
		{
			_mdEngineerNameRepository.Update(module);
		}
	}
}

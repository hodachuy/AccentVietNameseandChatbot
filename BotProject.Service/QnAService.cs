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
    public interface IQnAService
    {
		BotQnAnswer AddBotQnAnswer(ref BotQnAnswer botQnAnswer);
        QuestionGroup AddQuesGroup(QuestionGroup quesGroup);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);

        IEnumerable<QuestionGroup> GetListQuestionGroupByBotQnAnswerID(int botQnAnswerID);
		IEnumerable<BotQnAnswer> GetListBotQnAnswerByBotID(int botID);

		BotQnAnswer GetBotQnAnswerById(int id);

	}
    public class QnAService : IQnAService
    {
        IUnitOfWork _unitOfWork;
        IQuestionRepository _questionRepository;
        IAnswerRepository _answerRepository;
        IQuestionGroupRepository _quesGroupRepository;
		IBotQnAnswerRepository _botQnAnswerRepository;
        public QnAService(IUnitOfWork unitOfWork,
						  IBotQnAnswerRepository botQnAnswerRepository,
						  IQuestionRepository questionRepository,
                          IAnswerRepository answerRepository,
                          IQuestionGroupRepository quesGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _quesGroupRepository = quesGroupRepository;
			_botQnAnswerRepository = botQnAnswerRepository;

		}

        public QuestionGroup AddQuesGroup(QuestionGroup quesGroup)
        {
            return _quesGroupRepository.Add(quesGroup);
        }

        public Question AddQuestion(Question question)
        {
            return _questionRepository.Add(question);
        }

        public Answer AddAnswer(Answer answer)
        {
            return _answerRepository.Add(answer);
        }

        public IEnumerable<QuestionGroup> GetListQuestionGroupByBotQnAnswerID(int botQnAnswerID)
        {
			return _quesGroupRepository.GetMulti(x => x.BotQnAnswerID == botQnAnswerID);
		}

		public BotQnAnswer AddBotQnAnswer(ref BotQnAnswer botQnAnswer)
		{
			try
			{
				_botQnAnswerRepository.Add(botQnAnswer);
				_unitOfWork.Commit();
				return botQnAnswer;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public IEnumerable<BotQnAnswer> GetListBotQnAnswerByBotID(int botID)
		{
			return _botQnAnswerRepository.GetMulti(x => x.BotID == botID);
		}

		public BotQnAnswer GetBotQnAnswerById(int id)
		{
			return _botQnAnswerRepository.GetSingleById(id);
		}
	}
}

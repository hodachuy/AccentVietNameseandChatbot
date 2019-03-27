using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BotProject.Service
{
    public interface IQnAService
    {
		BotQnAnswer AddBotQnAnswer(ref BotQnAnswer botQnAnswer);
        QuestionGroup AddQuesGroup(QuestionGroup quesGroup);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);

        void UpdateQuestion(Question question);
        void UpdateAnswer(Answer answer);

        Question DeleteQuestion(int id);
        Answer DeleteAnswer(int id);

        QuestionGroup DeleteQuestionGroup(int id);

        IEnumerable<QuestionGroup> GetListQuestionGroupByBotQnAnswerID(int botQnAnswerID);
        IEnumerable<QuestionGroup> GetListQuesGroupToAimlByQnaID(int botQnAnswerID);
		IEnumerable<BotQnAnswer> GetListBotQnAnswerByBotID(int botID);

		IEnumerable<Question> GetListQuesCodeSymbol(string symbol);

		BotQnAnswer GetBotQnAnswerById(int id);

        void Save();

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
            var lstQuesGroup = _quesGroupRepository.GetMulti(x => x.BotQnAnswerID == botQnAnswerID).ToList();
            if(lstQuesGroup.Count != 0)
            {
                foreach(var item in lstQuesGroup)
                {
                    item.Questions = _questionRepository.GetMulti(x => x.QuestionGroupID == item.ID && x.IsThatStar == false).ToList();//hiển thị lên giao diện k lấy dấu *
                    item.Answers = _answerRepository.GetMulti(x => x.QuestionGroupID == item.ID).ToList();
                }
            }
            return lstQuesGroup;
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

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateQuestion(Question question)
        {
            _questionRepository.Update(question);
        }

        public void UpdateAnswer(Answer answer)
        {
            _answerRepository.Update(answer);
        }

        public Question DeleteQuestion(int id)
        {
            return _questionRepository.Delete(id);
        }

        public Answer DeleteAnswer(int id)
        {
            return _answerRepository.Delete(id);
        }

        public QuestionGroup DeleteQuestionGroup(int id)
        {
            _questionRepository.DeleteMulti(x => x.QuestionGroupID == id);
            _answerRepository.DeleteMulti(x => x.QuestionGroupID == id);
            return _quesGroupRepository.Delete(id);
        }

		public IEnumerable<Question> GetListQuesCodeSymbol(string symbol)
		{
			return _questionRepository.GetMulti(x => x.CodeSymbol.Contains(symbol));
		}

        public IEnumerable<QuestionGroup> GetListQuesGroupToAimlByQnaID(int botQnAnswerID)
        {
            var lstQuesGroup = _quesGroupRepository.GetMulti(x => x.BotQnAnswerID == botQnAnswerID).ToList();
            if (lstQuesGroup.Count != 0)
            {
                foreach (var item in lstQuesGroup)
                {
                    item.Questions = _questionRepository.GetMulti(x => x.QuestionGroupID == item.ID).ToList();
                    item.Answers = _answerRepository.GetMulti(x => x.QuestionGroupID == item.ID).ToList();
                }
            }
            return lstQuesGroup;
        }
    }
}

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
        QuestionGroup AddQuesGroup(QuestionGroup quesGroup);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);

        IEnumerable<QuestionGroup> GetAllQnAByBotID(int botID);

    }
    public class QnAService : IQnAService
    {
        IUnitOfWork _unitOfWork;
        IQuestionRepository _questionRepository;
        IAnswerRepository _answerRepository;
        IQuestionGroupRepository _quesGroupRepository;
        public QnAService(IUnitOfWork unitOfWork,
                          IQuestionRepository questionRepository,
                          IAnswerRepository answerRepository,
                          IQuestionGroupRepository quesGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _quesGroupRepository = quesGroupRepository;
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

        public IEnumerable<QuestionGroup> GetAllQnAByBotID(int botID)
        {
            throw new NotImplementedException();
        }
    }
}

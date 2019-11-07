﻿using BotProject.Common.ViewModels;
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
		FormQuestionAnswer AddFormQnAnswer(ref FormQuestionAnswer formQnAnswer);
        QuestionGroup AddQuesGroup(QuestionGroup quesGroup);
        Question AddQuestion(Question question);
        Answer AddAnswer(Answer answer);

        void UpdateQuestion(Question question);
        void UpdateAnswer(Answer answer);
        void UpdateFormQuestionAnswer(FormQuestionAnswer formQnAnswer);

        Question DeleteQuestion(int id);
        void DeleteQuesByQuestionGroup(int qGroupID);
        Answer DeleteAnswer(int id);
        void DeleteAnswerByQuestionGroup(int qGroupID);

        QuestionGroup DeleteQuestionGroup(int id);
        void DeleteMultiQuestionGroupByFormID(int formId);

        IEnumerable<QuestionGroup> GetListQuestionGroupByBotID(int botID);
        IEnumerable<QuestionGroup> GetListQuestionGroupByFormQnAnswerID(int formQnAnswerID);
        IEnumerable<QuestionGroup> GetListQuesGroupToAimlByFormQnAnswerID(int formQnAnwerID);
		IEnumerable<FormQuestionAnswer> GetListFormByBotID(int botID);

		IEnumerable<Question> GetListQuesCodeSymbol(string symbol);

        QuesTargetViewModel GetQuesByTarget(string target, int botID);

		FormQuestionAnswer GetFormQnAnswerById(int id);

        void Save();

	}
    public class QnAService : IQnAService
    {
        IUnitOfWork _unitOfWork;
        IQuestionRepository _questionRepository;
        IAnswerRepository _answerRepository;
        IQuestionGroupRepository _quesGroupRepository;
        IFormQuestionAnswerRepository _formQuestionAnswerRepository;
        public QnAService(IUnitOfWork unitOfWork,
                          IFormQuestionAnswerRepository formQuestionAnswerRepository,
						  IQuestionRepository questionRepository,
                          IAnswerRepository answerRepository,
                          IQuestionGroupRepository quesGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _quesGroupRepository = quesGroupRepository;
            _formQuestionAnswerRepository = formQuestionAnswerRepository;

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

        public IEnumerable<QuestionGroup> GetListQuestionGroupByFormQnAnswerID(int formQnAnswerID)
        {
            var lstQuesGroup = _quesGroupRepository.GetMulti(x => x.FormQuestionAnswerID == formQnAnswerID).ToList();
            if(lstQuesGroup.Count != 0)
            {
                foreach(var item in lstQuesGroup)
                {
                    item.Questions = _questionRepository.GetMulti(x => x.QuestionGroupID == item.ID && x.IsThatStar == false);
                    item.Answers = _answerRepository.GetMulti(x => x.QuestionGroupID == item.ID);
                }
            }
            return lstQuesGroup;
        }

		public FormQuestionAnswer AddFormQnAnswer(ref FormQuestionAnswer formQnAnswer)
		{
			try
			{
                _formQuestionAnswerRepository.Add(formQnAnswer);
				_unitOfWork.Commit();
				return formQnAnswer;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public IEnumerable<FormQuestionAnswer> GetListFormByBotID(int botID)
		{
			return _formQuestionAnswerRepository.GetMulti(x => x.BotID == botID && x.IsDelete == false);
		}

		public FormQuestionAnswer GetFormQnAnswerById(int id)
		{
			return _formQuestionAnswerRepository.GetSingleById(id);
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

        public IEnumerable<QuestionGroup> GetListQuesGroupToAimlByFormQnAnswerID(int formQnAnwerID)
        {
            var lstQuesGroup = _quesGroupRepository.GetMulti(x => x.FormQuestionAnswerID == formQnAnwerID && x.IsKeyword == true).ToList();
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

        public void DeleteQuesByQuestionGroup(int qGroupID)
        {
            _questionRepository.DeleteMulti(x => x.QuestionGroupID == qGroupID);
        }

        public void DeleteAnswerByQuestionGroup(int qGroupID)
        {
            _answerRepository.DeleteMulti(x => x.QuestionGroupID == qGroupID);
        }

        public IEnumerable<QuestionGroup> GetListQuestionGroupByBotID(int botID)
        {
            return _quesGroupRepository.GetMulti(x => x.BotID == botID);
        }

        public void DeleteMultiQuestionGroupByFormID(int formId)
        {
            _quesGroupRepository.DeleteMulti(x => x.FormQuestionAnswerID == formId);
        }

        public QuesTargetViewModel GetQuesByTarget(string target, int botID)
        {
            return _questionRepository.GetQuesByTarget(target, botID);
        }

        public void UpdateFormQuestionAnswer(FormQuestionAnswer formQnAnswer)
        {
            _formQuestionAnswerRepository.Update(formQnAnswer);
        }
    }
}

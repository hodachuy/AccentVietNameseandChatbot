using BotProject.Common.ViewModels.LiveChat;
using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service.Livechat
{
    public interface IChatCommonSerivce
    {
        Thread AddThreadMessage();
        ThreadParticipant AddThreadParticipant(ThreadParticipant threadParticipant);
        IEnumerable<SP_CustomerJoin> GetCustomerJoinChatByChannelGroupID(string filter, string sort, int pageNumber, int pageSize, long? selectedID);
        bool CheckCustomerInThreadParticipant(string customerId);
        // Add Message
        Message AddMessage(Message msg);
        IEnumerable<Message> GetListMessage(string condition, int page, int pageSize, string sort);
        void Save();
    }
    public class ChatCommonService : IChatCommonSerivce
    {
        IThreadRepository _threadRepository;
        IThreadParticipantRepository _threadParticipantRepository;
        IMessageRepository _messageRepository;
        IUnitOfWork _unitOfWork;
        public ChatCommonService(IThreadRepository threadRepository,
                              IThreadParticipantRepository threadParticipantRepository,
                              IMessageRepository messageRepository,
                              IUnitOfWork unitOfWork)
        {
            _threadRepository = threadRepository;
            _threadParticipantRepository = threadParticipantRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public Thread AddThreadMessage()
        {
            Thread thread = new Thread();
            return _threadRepository.Add(thread);
        }

        public ThreadParticipant AddThreadParticipant(ThreadParticipant threadParticipant)
        {
            return _threadParticipantRepository.Add(threadParticipant);
        }

        public IEnumerable<SP_CustomerJoin> GetCustomerJoinChatByChannelGroupID(string filter,string sort, int pageNumber, int pageSize, long? selectedID)
        {
            return _threadParticipantRepository.GetCustomerJoinChatChannel(filter, sort, pageNumber, pageSize, selectedID);
        }

        public Message AddMessage(Message msg)
        {
            return _messageRepository.Add(msg);
        }

        public IEnumerable<Message> GetListMessage(string condition, int page, int pageSize, string sort)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool CheckCustomerInThreadParticipant(string customerId)
        {
            return _threadParticipantRepository.CheckContains(x => x.CustomerID == customerId);
        }
    }
}

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
    public interface IChatHubSerivce
    {
        Thread AddThreadMessage();
        ThreadParticipantRepository AddThreadParticipant(ThreadParticipant threadParticipant);
        ThreadParticipantRepository GetThreadParticipantByUser(string userId, long groupChannelID);

        // Add Message
        Message AddMessage(Message msg);
        IEnumerable<Message> GetListMessage(string condition, int page, int pageSize, string sort);
        void Save();
    }
    public class ChatHubService : IChatHubSerivce
    {
        IThreadRepository _threadRepository;
        IThreadParticipantRepository _threadParticipantRepository;
        IMessageRepository _messageRepository;
        IUnitOfWork _unitOfWork;
        public ChatHubService(IThreadRepository threadRepository,
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

        public ThreadParticipantRepository AddThreadParticipant(ThreadParticipant threadParticipant)
        {
            throw new NotImplementedException();
        }

        public ThreadParticipantRepository GetThreadParticipantByUser(string userId, long groupChannelID)
        {
            throw new NotImplementedException();
        }

        public Message AddMessage(Message msg)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetListMessage(string condition, int page, int pageSize, string sort)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}

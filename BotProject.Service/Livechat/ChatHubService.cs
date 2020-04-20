using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service.Livechat
{
    public interface IChatHubSerivce
    {
        Thread AddThreadMessage();
        ThreadParticipantRepository AddThreadParticipant(ThreadParticipant threadParticipant);
        ThreadParticipantRepository GetThreadParticipantByUser(string userId, long groupChannelID);

        // Add Message
        Message AddMessage(Message msg);
        IEnumerable<Message> GetListMessage(string condition, int page, int pageSize, string sort);
    }
    public class ChatHubService : IChatHubSerivce
    {
        private IThreadRepository _threadRepository;
        private IThreadParticipantRepository _threadParticipantRepository;
        private IMessageRepository _messageRepository;
        public ChatHubService(IThreadRepository threadRepository,
                              IThreadParticipantRepository threadParticipantRepository,
                              IMessageRepository messageRepository)
        {
            _threadRepository = threadRepository;
            _threadParticipantRepository = threadParticipantRepository;
            _messageRepository = messageRepository;
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
    }
}

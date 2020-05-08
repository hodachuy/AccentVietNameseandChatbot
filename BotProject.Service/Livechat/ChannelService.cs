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

namespace BotProject.Service
{
    public interface IChannelService
    {
        IEnumerable<SP_Channel> GetListChannelByChannelGroupID(int channelGroupID);
        ChannelGroup AddChannelGroup(ChannelGroup channelGroup);
        ChannelGroup GetChannelGroupById(int channelGroupID);
        Channel AddUserToChannel(Channel channel);
        Channel GetChannelByUserId(string userId);
        void RemoveUserChannel(string userId);
        void Save();
    }
    public class ChannelService : IChannelService
    {
        IChannelRepository _channelRepository;
        IChannelGroupRepository _channelGroupRepository;
        IUnitOfWork _unitOfWork;
        public ChannelService(IChannelRepository channelRepository,
                              IChannelGroupRepository channelGroupRepository,
                              IUnitOfWork unitOfWork)
        {
            _channelRepository = channelRepository;
            _channelGroupRepository = channelGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public ChannelGroup AddChannelGroup(ChannelGroup grChannel)
        {
            return _channelGroupRepository.Add(grChannel);
        }

        public Channel AddUserToChannel(Channel channel)
        {
            return _channelRepository.Add(channel);
        }

        public Channel GetChannelByUserId(string userId)
        {
            return _channelRepository.GetSingleByCondition(x => x.UserID == userId);
        }

        public ChannelGroup GetChannelGroupById(int groupChannelID)
        {
            return _channelGroupRepository.GetSingleById(groupChannelID);
        }

        public IEnumerable<SP_Channel> GetListChannelByChannelGroupID(int groupChannelID)
        {
            return _channelRepository.sp_GetListChannel(groupChannelID);
        }

        public void RemoveUserChannel(string userId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}

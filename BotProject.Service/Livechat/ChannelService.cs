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
        IEnumerable<SP_Channel> GetListChannelByGorupChanelID(int groupChannelID);
        GroupChannel AddGroupChannel(GroupChannel grChannel);
        GroupChannel GetGroupChannelById(int groupChannelID);
        Channel AddUserToChannel(Channel channel);
        Channel GetChannelByUserId(string userId);
        void RemoveUserChannel(string userId);
        void Save();
    }
    public class ChannelService : IChannelService
    {
        IChannelRepository _channelRepository;
        IGroupChannelRepository _groupChannelRepository;
        IUnitOfWork _unitOfWork;
        public ChannelService(IChannelRepository channelRepository,
                              IGroupChannelRepository groupChannelRepository,
                              IUnitOfWork unitOfWork)
        {
            _channelRepository = channelRepository;
            _groupChannelRepository = groupChannelRepository;
            _unitOfWork = unitOfWork;
        }

        public GroupChannel AddGroupChannel(GroupChannel grChannel)
        {
            return _groupChannelRepository.Add(grChannel);
        }

        public Channel AddUserToChannel(Channel channel)
        {
            return _channelRepository.Add(channel);
        }

        public Channel GetChannelByUserId(string userId)
        {
            return _channelRepository.GetSingleByCondition(x => x.UserID == userId);
        }

        public GroupChannel GetGroupChannelById(int groupChannelID)
        {
            return _groupChannelRepository.GetSingleById(groupChannelID);
        }

        public IEnumerable<SP_Channel> GetListChannelByGorupChanelID(int groupChannelID)
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

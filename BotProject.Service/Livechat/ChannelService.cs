using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service.Livechat
{
    public interface IChannelService
    {
        IEnumerable<Channel> GetListChannelByGorupChanelID(long groupChannelID);
        GroupChannel AddGroupChannel(GroupChannel grChannel);
        GroupChannel GetGroupChannelById(long groupChannelID);
        Channel AddUserToChannel(Channel channel);
        void RemoveUserChannel(string userId);
    }
    public class ChannelService : IChannelService
    {
        private IChannelRepository _channelRepository;
        private IGroupChannelRepository _groupChannelRepository;
        private UnitOfWork _unitOfWork;
        public ChannelService(IChannelRepository channelRepository,
                              IGroupChannelRepository groupChannelRepository,
                              UnitOfWork unitOfWork)
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

        public GroupChannel GetGroupChannelById(long groupChannelID)
        {
            return _groupChannelRepository.GetSingleById(Convert.ToInt32(groupChannelID));
        }

        public IEnumerable<Channel> GetListChannelByGorupChanelID(long groupChannelID)
        {
            return _channelRepository.GetMulti(x => x.GroupChannelID == groupChannelID);
        }

        public void RemoveUserChannel(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

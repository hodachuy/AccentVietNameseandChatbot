﻿using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IGroupChannelRepository : IRepository<GroupChannel>
    {
    }

    public class GroupChannelRepository : RepositoryBase<GroupChannel>, IGroupChannelRepository
    {
        public GroupChannelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

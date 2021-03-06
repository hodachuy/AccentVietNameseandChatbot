﻿using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IAIMLFileRepository : IRepository<AIMLFile>
    {
    }

    public class AIMLFileRepository : RepositoryBase<AIMLFile>, IAIMLFileRepository
    {
        public AIMLFileRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

﻿using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IChatWidgetLanguageRepository : IRepository<ChatWidgetLanguage>
    {

    }
    public class ChatWidgetLanguageRepository : RepositoryBase<ChatWidgetLanguage>, IChatWidgetLanguageRepository
    {
        public ChatWidgetLanguageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

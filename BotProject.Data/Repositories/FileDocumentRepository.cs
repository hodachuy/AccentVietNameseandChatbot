﻿using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IFileDocumentRepository : IRepository<FileDocument>
    {
    }

    public class FileDocumentRepository : RepositoryBase<FileDocument>, IFileDocumentRepository
    {
        public FileDocumentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

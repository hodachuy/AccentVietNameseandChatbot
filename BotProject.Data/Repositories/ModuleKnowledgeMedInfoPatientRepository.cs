using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IModuleKnowledgeMedInfoPatientRepository : IRepository<ModuleKnowledgeMedInfoPatient>
    {

    }

    public class ModuleKnowledgeMedInfoPatientRepository : RepositoryBase<ModuleKnowledgeMedInfoPatient>, IModuleKnowledgeMedInfoPatientRepository
    {
        public ModuleKnowledgeMedInfoPatientRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

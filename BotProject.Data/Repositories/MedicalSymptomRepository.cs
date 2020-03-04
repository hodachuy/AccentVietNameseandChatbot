using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMedicalSymptomRepository : IRepository<MedicalSymptom>
    {

    }
    public class MedicalSymptomRepository : RepositoryBase<MedicalSymptom>, IMedicalSymptomRepository
    {
        public MedicalSymptomRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using BotProject.Data.Infrastructure;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Data.Repositories
{
    public interface IMdVoucherRepository : IRepository<MdVoucher>
    {
    }

    public class MdVoucherRepository : RepositoryBase<MdVoucher>, IMdVoucherRepository
    {
        public MdVoucherRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

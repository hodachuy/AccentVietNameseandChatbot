using BotProject.Data.Infrastructure;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Data.Repositories
{
    public interface IDeviceRepository : IRepository<Device>
    {
    }

    public class DeviceRepository : RepositoryBase<Device>, IDeviceRepository
    {
        public DeviceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

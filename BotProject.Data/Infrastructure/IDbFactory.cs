using System;

namespace BotProject.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BotDbContext Init();
    }
}
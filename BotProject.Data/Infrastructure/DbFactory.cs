namespace BotProject.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private BotDbContext dbContext;

        public BotDbContext Init()
        {
            return dbContext ?? (dbContext = new BotDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
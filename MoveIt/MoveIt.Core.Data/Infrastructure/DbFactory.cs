namespace MoveIt.Core.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        MoveItEntities dbContext;

        public MoveItEntities Init()
        {
            return dbContext ?? (dbContext = new MoveItEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

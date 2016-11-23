using System;

namespace MoveIt.Core.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        MoveItEntities Init();
    }
}

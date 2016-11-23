using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        IQueryable<Client> GetAllClients();
        Client GetClient(long id);
        Task<Client> GetClientAsync(long id);
        bool ClientExists(long clientID);

        bool ClientExists(string email);
        Client GetClient(string email);
    }
}

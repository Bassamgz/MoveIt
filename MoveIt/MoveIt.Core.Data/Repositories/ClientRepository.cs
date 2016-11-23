using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IQueryable<Client> GetAllClients()
        {
            var allClients =
                DbContext.Clients;

            return allClients;
        }

        public Client GetClient(long id)
        {
            var client = DbContext.Clients.FirstOrDefault(item => item.ID == id);
            return client;
        }

        public Task<Client> GetClientAsync(long id)
        {
            var client = DbContext.Clients.FirstOrDefaultAsync(item => item.ID == id);
            return client;
        }

        public bool ClientExists(long id)
        {
            return DbContext.Clients.Count(e => e.ID == id) > 0;
        }

        public bool ClientExists(string email)
        {
            return DbContext.Clients.Count(e => e.Email == email) > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Service
{
    public interface IClientService
    {
        IQueryable<Client> GetAllClients();

        Client GetClient(long id);

        Task<Client> GetClientAsync(long id);

        void CreateClient(Client client);
        void SaveClient();
        bool ClientExists(long clientID);

        bool ClientExists(string email);
        void UpdateClient(Client client);
        Client GetClient(string email);
    }
}

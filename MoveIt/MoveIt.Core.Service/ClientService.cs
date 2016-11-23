using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;
using MoveIt.Core.Data.Repositories;

namespace MoveIt.Core.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientsRepository, IUnitOfWork unitOfWork)
        {
            this._clientRepository = clientsRepository;
            this._unitOfWork = unitOfWork;
        }

        public IQueryable<Client> GetAllClients()
        {
            var allClient = _clientRepository.GetAllClients();
            return allClient;
        }

        public Client GetClient(long id)
        {
            var client = _clientRepository.GetClient(id);
            return client;
        }

        public Task<Client> GetClientAsync(long id)
        {
            var client = _clientRepository.GetClientAsync(id);
            return client;
        }

        public void CreateClient(Client client)
        {
            _clientRepository.Add(client);
        }

        public void SaveClient()
        {
            _unitOfWork.Commit();
        }

        public bool ClientExists(long clientID)
        {
            return _clientRepository.ClientExists(clientID);
        }

        public bool ClientExists(string email)
        {
            return _clientRepository.ClientExists(email);
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);
        }

        public Client GetClient(string email)
        {
            var client = _clientRepository.GetClient(email);
            return client;
        }
    }
}

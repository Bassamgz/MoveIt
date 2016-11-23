using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MoveIt.Core.Data.Model;
using MoveIt.Core.Data.Model.DTO;
using MoveIt.Core.Service;

namespace MoveIt.API.MoveItService.Controllers
{
    public class ClientsController : ApiController
    {
        private readonly IClientService _clientService;


        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;

        }

        // GET: api/Clients
        [AcceptVerbs("GET")]
        [HttpGet]
        public IQueryable<ClientDTO> GetClients()
        {
            var clients = _clientService.GetAllClients();
            var dtoclients = clients.ProjectTo<ClientDTO>();
            return dtoclients;
        }

        // GET: api/Clients/5
        [AcceptVerbs("GET")]
        [HttpGet]
        [ResponseType(typeof(ClientDTO))]
        public async Task<IHttpActionResult> GetClient(long id)
        {
            Client client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var dtoClient = Mapper.Map<ClientDTO>(client);
            return Ok(dtoClient);
        }

        // PUT: api/Clients/5
        [AcceptVerbs("PUT")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var proposal = Mapper.Map<Client>(client);
            _clientService.UpdateClient(proposal);

            try
            {
                _clientService.SaveClient();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_clientService.ClientExists(client.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [AcceptVerbs("POST")]
        [HttpPost]
        [ResponseType(typeof(ClientDTO))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _clientService.CreateClient(client);
            try
            {
                _clientService.SaveClient();
            }
            catch (DbUpdateException)
            {
                if (!_clientService.ClientExists(client.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var dtoClient = Mapper.Map<ClientDTO>(client);
            return CreatedAtRoute("DefaultApi", new { id = dtoClient.ID }, dtoClient);
        }


    }
}
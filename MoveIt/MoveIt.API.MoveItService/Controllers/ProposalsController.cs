using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using MoveIt.Core.Data.Model;
using MoveIt.Core.Data.Model.DTO;
using MoveIt.Core.Service;
using AutoMapper.QueryableExtensions;

namespace MoveIt.API.MoveItService.Controllers
{
    public class ProposalsController : ApiController
    {

        private readonly IProposalService _proposalService;
        private readonly IClientService _clientService;

        public ProposalsController(IProposalService proposalService, IClientService clientService)
        {
            _proposalService = proposalService;
            _clientService = clientService;

        }

        // GET: api/Proposals
        [AcceptVerbs("GET")]
        [HttpGet]
        public IQueryable<ProposalDTO> GetClientProposals(string email)
        {
            var proposals = _proposalService.GetAllProposals(email);
            var dtoProposal = proposals.ProjectTo<ProposalDTO>();
            return dtoProposal;
        }

        // GET: api/Proposals
        [AcceptVerbs("GET")]
        [HttpGet]
        public IQueryable<ProposalDTO> GetOffers(string id, string isAccepted)
        {
            var proposals = _proposalService.GetClientOffers(id, isAccepted);
            var dtoProposal = proposals.ProjectTo<ProposalDTO>();
            return dtoProposal;
        }

        // GET: api/Proposals/5
        [AcceptVerbs("GET")]
        [HttpGet]
        [ResponseType(typeof(ProposalDTO))]
        public async Task<IHttpActionResult> GetProposal(Guid id)
        {
            Proposal proposal = await _proposalService.GetProposalAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }
            var dtoProposal = Mapper.Map<ProposalDTO>(proposal);
            return Ok(dtoProposal);
        }

        // PUT: api/Proposals/5
        [AcceptVerbs("PUT")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProposal(ProposalDTO updatedProposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var proposal = Mapper.Map<Proposal>(updatedProposal);
            _proposalService.UpdateProposal(proposal);

            try
            {
                _proposalService.SaveProposal();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_proposalService.ProposalExists(proposal.ID))
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

        // POST: api/Proposals
        [AcceptVerbs("POST")]
        [HttpPost]
        [ResponseType(typeof(ProposalDTO))]
        public IHttpActionResult PostProposal(ProposalDTO dtoProposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var proposal = Mapper.Map<Proposal>(dtoProposal);
            _proposalService.CreateProposal(proposal);

            try
            {
                _proposalService.SaveProposal();
            }
            catch (DbUpdateException)
            {
                if (!_proposalService.ProposalExists(proposal.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = dtoProposal.ID }, dtoProposal);
        }

        // DELETE: api/Proposals
        [AcceptVerbs("DELETE")]
        [HttpDelete]
        [ResponseType(typeof(ProposalDTO))]
        public void DeleteProposal(Guid id)
        {
            if (_proposalService.ProposalExists(id))
            {
                {
                    _proposalService.DeleteProposal(id);

                    _proposalService.SaveProposal();
                }
            }

        }

    }
}
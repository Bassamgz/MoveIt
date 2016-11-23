using System;
using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;
using MoveIt.Core.Data.Repositories;

namespace MoveIt.Core.Service
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProposalService(IProposalRepository proposalRepository, IUnitOfWork unitOfWork)
        {
            this._proposalRepository = proposalRepository;
            this._unitOfWork = unitOfWork;
        }

        public IQueryable<Proposal> GetAllProposals(string id)
        {
            var proposals = _proposalRepository.GetAllProposals(id);
            return proposals;
        }

        public IQueryable<Proposal> GetClientProposals(long id)
        {
            var clientProposals = _proposalRepository.GetClientProposals(id);
            return clientProposals;
        }

        public IQueryable<Proposal> GetClientOffers(string id, string isAccepted)
        {
            var clientProposals = _proposalRepository.GetClientOffers(id,isAccepted);
            return clientProposals;
        }

        public void DeleteProposal(Guid id)
        {
            _proposalRepository.Delete(GetProposal(id));
        }

        public Proposal GetProposal(Guid id)
        {
            var proposal = _proposalRepository.GetProposal(id);
            return proposal;
        }

        public Task<Proposal> GetProposalAsync(Guid id)
        {
            var proposal = _proposalRepository.GetProposalAsync(id);
            return proposal;
        }

        public void CreateProposal(Proposal proposal)
        {
            _proposalRepository.Add(proposal);
        }

        public void SaveProposal()
        {
            _unitOfWork.Commit();
        }

        public bool ProposalExists(Guid proposalID)
        {
            return _proposalRepository.ProposalExists(proposalID);
        }

        public void UpdateProposal(Proposal proposal)
        {
            _proposalRepository.Update(proposal);
        }

        
    }
}

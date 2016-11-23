using System;
using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Service
{
    public interface IProposalService
    {
        IQueryable<Proposal> GetAllProposals(string id);

        IQueryable<Proposal> GetClientProposals(long id);

        Proposal GetProposal(Guid id);

        Task<Proposal> GetProposalAsync(Guid id);

        void CreateProposal(Proposal Proposal);
        void SaveProposal();
        bool ProposalExists(Guid proposalID);
        void UpdateProposal(Proposal proposal);
        IQueryable<Proposal> GetClientOffers(string id, string isAccepted);
        void DeleteProposal(Guid id);
    }
}

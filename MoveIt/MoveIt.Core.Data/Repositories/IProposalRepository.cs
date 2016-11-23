using System;
using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data.Repositories
{
    public interface IProposalRepository : IRepository<Proposal>
    {
        IQueryable<Proposal> GetAllProposals(string id);
        IQueryable<Proposal> GetClientProposals(long id);
        Proposal GetProposal(Guid id);
        Task<Proposal> GetProposalAsync(Guid id);
        bool ProposalExists(Guid id);
        IQueryable<Proposal> GetClientOffers(string id, string isAccepted);
    }
}

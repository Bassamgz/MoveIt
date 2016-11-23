using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoveIt.Core.Data.Infrastructure;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data.Repositories
{
    public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
    {
        public ProposalRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IQueryable<Proposal> GetAllProposals(string id)
        {
            var allProposals = DbContext.Proposals.Join(DbContext.Clients.Where(item => item.Email == id),
                first => first.ClientID,
                second => second.ID,
                (first, second) => new { first }).Select(item => item.first).Where(item => item.IsAccepted == false);
            return allProposals;
        }

        public IQueryable<Proposal> GetClientProposals(long id)
        {
            var clientProposals = DbContext.Proposals.Where(item => item.Client.ID == id);
            return clientProposals;
        }

        public IQueryable<Proposal> GetClientOffers(string id, string isAccepted)
        {
            bool status = false;

            status = isAccepted == "1";

            var clientOffers =
                DbContext.Proposals.Where(item => item.Client.Email == id && item.IsAccepted == status);
            return clientOffers;
        }

        public Proposal GetProposal(Guid id)
        {
            var proposal = DbContext.Proposals.SingleOrDefault(item => item.ID == id);
            return proposal;
        }

        public Task<Proposal> GetProposalAsync(Guid id)
        {
            var proposal = DbContext.Proposals.SingleOrDefaultAsync(item => item.ID == id);
            return proposal;
        }

        public bool ProposalExists(Guid id)
        {
            return DbContext.Proposals.Count(e => e.ID == id) > 0;
        }


    }
}

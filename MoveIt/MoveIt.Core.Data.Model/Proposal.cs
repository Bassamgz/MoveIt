using System;

namespace MoveIt.Core.Data.Model
{
    public class Proposal
    {
        public Guid ID { get; set; }

        public long ClientID { get; set; }

        public decimal Distance { get; set; }

        public decimal Volume { get; set; }

        public bool HasPiano { get; set; }

        public DateTime ProposalDate { get; set; }

        public DateTime? AcceptanceDate { get; set; }

        public bool IsAccepted { get; set; }

        public decimal? Price { get; set; }

        public virtual Client Client { get; set; }
    }
}

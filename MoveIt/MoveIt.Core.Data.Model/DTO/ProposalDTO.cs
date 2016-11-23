using System;
using System.ComponentModel;

namespace MoveIt.Core.Data.Model.DTO
{
    public class ProposalDTO
    {
        public Guid ID { get; set; }

        [DisplayName("Client ID")]
        public long ClientID { get; set; }

        public string Email { get; set; }

        [DisplayName("Distance (km)")]
        public decimal Distance { get; set; }

        [DisplayName("Volume (m^2)")]
        public decimal Volume { get; set; }

        [DisplayName("Has Piano")]
        public bool HasPiano { get; set; }

        [DisplayName("Proposal Date")]
        public DateTime ProposalDate { get; set; }

        [DisplayName("Acceptance Date")]
        public DateTime? AcceptanceDate { get; set; }

        [DisplayName("Is Accepted")]
        public bool IsAccepted { get; set; }

        public decimal? Price { get; set; }

        public virtual Client Client { get; set; }
    }
}

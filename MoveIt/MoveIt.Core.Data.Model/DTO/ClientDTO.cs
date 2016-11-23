using System.Collections.Generic;
using System.ComponentModel;

namespace MoveIt.Core.Data.Model.DTO
{
    public class ClientDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [DisplayName("Building Number")]
        public int BuildingNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}

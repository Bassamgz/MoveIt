using System.Collections.Generic;

namespace MoveIt.Core.Data.Model
{
    public class Client
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int BuildingNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}

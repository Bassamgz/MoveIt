using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveIt.Core.Data.Configuration
{
    class ClientConfiguration : EntityTypeConfiguration<MoveIt.Core.Data.Model.Client>
    {
        public ClientConfiguration()
        {


            ToTable("Client");
            Property(g => g.Name).IsRequired().HasMaxLength(150);
            Property(g => g.Email).IsRequired().HasMaxLength(150);
            Property(g => g.Street).IsRequired().HasMaxLength(250);
            Property(g => g.PostalCode).IsRequired().HasMaxLength(6);
            Property(g => g.City).IsRequired().HasMaxLength(50);
            Property(g => g.Password).IsRequired().HasMaxLength(50);
        }
    }
}

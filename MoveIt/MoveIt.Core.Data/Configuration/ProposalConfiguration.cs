using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveIt.Core.Data.Configuration
{
    class ProposalConfiguration : EntityTypeConfiguration<MoveIt.Core.Data.Model.Proposal>
    {
        public ProposalConfiguration()
        {
            ToTable("Proposal");
            Property(g => g.ID)
                .HasColumnOrder(0)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            Property(g => g.ClientID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnOrder(1)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
        }
    }
}

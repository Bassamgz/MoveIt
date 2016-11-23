using System.Data.Entity.Validation;
using System.Text;
using MoveIt.Core.Data.Configuration;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MoveItEntities : DbContext
    {
        public MoveItEntities()
            : base("name=MoveItEntities")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Proposal> Proposals { get; set; }

        public virtual void Commit()
        {

            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new ProposalConfiguration());

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Proposals)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Distance)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Volume)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}

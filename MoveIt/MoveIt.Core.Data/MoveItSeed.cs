using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using MoveIt.Core.Data.Model;

namespace MoveIt.Core.Data
{
    public class MoveItSeed : DropCreateDatabaseIfModelChanges<MoveItEntities>
    {

        protected override void Seed(MoveItEntities context)
        {
            context.Clients.AddOrUpdate(x => x.ID,
                new Client()
                {
                    ID = 1,
                    Name = "John Holmberg",
                    BuildingNumber = 8,
                    City = "Södertälje",
                    Street = "Cementvägen",
                    PostalCode = "11111",
                    Email = "John.Holmberg@gmail.com",
                    Password = "p@ssw0rd"

                },
                new Client()
                {
                    ID = 2,
                    Name = "Fred Nils",
                    BuildingNumber = 12,
                    City = "Stockholm",
                    Street = "Balkvägen",
                    PostalCode = "22222",
                    Email = "Fred.Nils@gmail.com",
                    Password = "p@ssw0rd"
                },
                new Client()
                {
                    ID = 3,
                    Name = "Niklas Aus",
                    BuildingNumber = 1,
                    City = "Uppsala",
                    Street = "Budgetvägen",
                    PostalCode = "33333",
                    Email = "Niklas.Aus@gmail.com",
                    Password = "p@ssw0rd"
                });

            context.Proposals.AddOrUpdate(
                new Proposal
                {
                    ClientID = 1,
                    Distance = 20,
                    Volume = 50,
                    HasPiano = false,
                    ID = Guid.NewGuid(),
                    IsAccepted = false,
                    ProposalDate = DateTime.Now
                },
                new Proposal
                {
                    ClientID = 2,
                    Distance = 30,
                    Volume = 30,
                    HasPiano = true,
                    ID = Guid.NewGuid(),
                    IsAccepted = false,
                    ProposalDate = DateTime.Now
                },
                new Proposal
                {
                    ClientID = 3,
                    Distance = 10,
                    Volume = 20,
                    HasPiano = true,
                    ID = Guid.NewGuid(),
                    IsAccepted = true,
                    ProposalDate = DateTime.Now.AddDays(-1),
                    AcceptanceDate = DateTime.Now
                }
            );

            context.Commit();
        }


    }
}


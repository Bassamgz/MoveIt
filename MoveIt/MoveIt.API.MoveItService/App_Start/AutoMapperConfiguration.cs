using AutoMapper;
using MoveIt.Core.Data.Model;
using MoveIt.Core.Data.Model.DTO;

namespace MoveIt.API.MoveItService
{
    public static class AutoMapperConfigurartion
    {
        public static void BootStrap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<ClientDTO, Client>();
                cfg.CreateMap<Proposal, ProposalDTO>();
                cfg.CreateMap<ProposalDTO, Proposal>();
                
            }
            );


        }
    }
}
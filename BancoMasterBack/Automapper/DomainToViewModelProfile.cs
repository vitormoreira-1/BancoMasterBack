using AutoMapper;
using BancoMasterBack.Webapi.DTOs;

namespace BancoMasterBack.Webapi.Automapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Route, RouteViewModel>();
        }
    }
}

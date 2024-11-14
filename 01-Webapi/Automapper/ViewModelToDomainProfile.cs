using AutoMapper;
using BancoMasterBack.Webapi.DTOs;

namespace BancoMasterBack.Webapi.Automapper
{
    public class ViewModelToViewModelProfile : Profile
    {
        public ViewModelToViewModelProfile()
        {
            CreateMap<RouteViewModel, Domain.Entities.Route>();
        }
    }
}

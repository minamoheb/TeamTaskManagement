using AutoMapper;
using TeamTaskManagement.Core.Entities;
using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Helper.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<LookupItems, LookupItemsModel>()
                .ForMember(dest => dest.LookupName, act => act.MapFrom(src => src.Lookups.Name)).ReverseMap();
            CreateMap<TaskModel, Tasks>();
            CreateMap<Tasks, TaskModel>()
              .ForMember(dest => dest.PriorityName, act => act.MapFrom(src => src.Priority.NameEn))
              .ForMember(dest => dest.StatusName, act => act.MapFrom(src => src.Status.NameEn));
        }
    }


}


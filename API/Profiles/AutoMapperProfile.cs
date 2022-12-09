using AutoMapper;
using API.Models;
using API.ViewModels;

namespace API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient, PatientViewModel>()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => src.Disease!.Start))
                .ForMember(dest => dest.End, opts => opts.MapFrom(src => src.Disease!.End))
                .ReverseMap();

            CreateMap<Patient, UpdatePatientViewModel>().ReverseMap();
        }
    }
}

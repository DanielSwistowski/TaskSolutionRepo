using AutoMapper;
using DataAccessLayer.Models;
using TaskSolution.ViewModels;

namespace TaskSolution.AutoMapperProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyViewModel>();

            CreateMap<AddCompanyViewModel, Company>();

            CreateMap<Company, EditCompanyViewModel>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.CompanyAddress.City))
                .ForMember(d => d.HouseNumber, o => o.MapFrom(s => s.CompanyAddress.HouseNumber))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.CompanyAddress.Street))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.CompanyAddress.ZipCode)).ReverseMap();

            CreateMap<Company, CompanyDetailsViewModel>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.CompanyAddress.City))
                .ForMember(d => d.HouseNumber, o => o.MapFrom(s => s.CompanyAddress.HouseNumber))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.CompanyAddress.Street))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.CompanyAddress.ZipCode));

            CreateMap<Company, CompanyDetailsApiViewModel>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.CompanyAddress.City))
                .ForMember(d => d.HouseNumber, o => o.MapFrom(s => s.CompanyAddress.HouseNumber))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.CompanyAddress.Street))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.CompanyAddress.ZipCode));
        }
    }
}
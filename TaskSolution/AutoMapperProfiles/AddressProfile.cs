using AutoMapper;
using DataAccessLayer.Models;
using TaskSolution.ViewModels;

namespace TaskSolution.AutoMapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddAddressViewModel, Address>();
        }
    }
}
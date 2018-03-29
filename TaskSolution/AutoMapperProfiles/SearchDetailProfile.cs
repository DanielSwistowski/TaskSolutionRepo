using AutoMapper;
using DataAccessLayer.Models;
using TaskSolution.ViewModels;

namespace TaskSolution.AutoMapperProfiles
{
    public class SearchDetailProfile : Profile
    {
        public SearchDetailProfile()
        {
            CreateMap<SearchDetail, SearchDetailViewModel>();
        }
    }
}
using ApiTest.Data;
using ApiTest.Data.DTOS;
using AutoMapper;

namespace ApiTest.config
{
    public class AutoMapperconfig : Profile
    {
        public AutoMapperconfig() 
        {
           // CreateMap<Category, CategoryDTO>().ForMember(n=>n.Name , opt => opt.MapFrom(x=>x.Name)).ReverseMap()
            CreateMap<Category, CategoryDTO>().ReverseMap().ForMember(n => n.Name, opt => opt.Ignore());
        }
    }
}

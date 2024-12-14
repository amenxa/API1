using ApiTest.Data;
using ApiTest.Data.DTOS;
using AutoMapper;

namespace ApiTest.config
{
    public class AutoMapperconfig : Profile
    {
        public AutoMapperconfig() 
        {
            // config to transform differente names 
            // CreateMap<Category, CategoryDTO>().ForMember(n=>n.Name , opt => opt.MapFrom(x=>x.Name)).ReverseMap()
            // config to  ignore transform  names
            //CreateMap<Category, CategoryDTO>().ReverseMap().ForMember(n => n.Name, opt => opt.Ignore());
            // config to transform and chang the transfared data 
            // CreateMap<Category, CategoryDTO>().ForMember(n => n.Name, opt => opt.MapFrom(x => x.Name))
            //    .AddTransform<string>(n => (n == "string") ? "pistashio" : n).ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}

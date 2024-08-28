using AutoMapper;
using Library_API.Models;
using Library_API.Models.DTOs;

namespace Library_API
{
    public class MappingConfig : Profile 
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
        }
    }
}

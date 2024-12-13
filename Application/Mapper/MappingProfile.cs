using Application.Dtos;
using AutoMapper;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {   
       public MappingProfile() 
        {
            CreateMap<RegisterDtos,AppUser>().ReverseMap();
        }
    }
}

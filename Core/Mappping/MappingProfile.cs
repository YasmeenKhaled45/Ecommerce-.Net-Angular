using System;
using AutoMapper;
using Core.DTOS;
using Core.Entities;

namespace Core.Mappping;

public class MappingProfile : Profile
{
  public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>();
    }
}

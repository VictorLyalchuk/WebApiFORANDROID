using Core.DTO.Category;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
    public class Mapper : AutoMapper.Profile
    {
        public Mapper()
        {
            CreateMap<CategoryDTO, CategoryEntity>().ReverseMap();
            CreateMap<CreateCategoryDTO, CategoryEntity>().ReverseMap();
            CreateMap<EditCategoryDTO, CategoryEntity>().ReverseMap();
        }
    }
}

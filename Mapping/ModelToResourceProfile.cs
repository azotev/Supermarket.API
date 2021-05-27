using System.Linq;
using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;
using Supermarket.API.Extensions;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>()
                .ForMember(dest => dest.ProductNames,
                    opt => opt.MapFrom
                        (src => src.Products.Select(p => p.Name)));

            CreateMap<Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom
                        (src => src.UnitOfMeasurement.ToDescriptionString()));
        }
    }
}

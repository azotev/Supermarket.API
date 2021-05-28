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
            CreateMap<Product, ProductResource>()
                .ForMember(dest => dest.OrderId,
                    opt => opt.MapFrom
                        (src => src.OrderItems.Select(o => o.OrderId)));

        }
    }
}

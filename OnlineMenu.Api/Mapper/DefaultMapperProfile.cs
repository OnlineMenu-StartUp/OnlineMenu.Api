using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Api.ViewModel.ProductExtra;
using OnlineMenu.Application.Dto;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Mapper
{
    public class DefaultMapperProfile: Profile
    {
        public DefaultMapperProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();

            CreateMap<ToppingShallowRequestModel, Topping>();
            CreateMap<Topping, ToppingShallowResponseModel>();
            CreateMap<ProductUpdateModel, Product>();
            
            CreateMap<ProductCreateModel, Product>()
                .ForMember(p => p.ToppingLinks, 
                    opt => opt.MapFrom<ConvertToProductExtras, ICollection<ToppingShallowRequestModel>>(src => src.Toppings));
            
            CreateMap<Product, ProductResponseModel>()
                .ForMember(pr => pr.Toppings,
                    opt => opt.MapFrom<ConvertFromProductExtra, ICollection<ProductTopping>?>(src => src.ToppingLinks));
            
        }
    }
    public class ConvertToProductExtras : IMemberValueResolver<ProductCreateModel, Product, ICollection<ToppingShallowRequestModel>, ICollection<ProductTopping>?>
    {

        public ICollection<ProductTopping>? Resolve(
            ProductCreateModel source, 
            Product destination, 
            ICollection<ToppingShallowRequestModel> sourceMember, 
            ICollection<ProductTopping>? destMember,
            ResolutionContext context)
        {
            List<Topping> productExtras =
                source.Toppings.Select(pesr => context.Mapper.Map<Topping>(pesr)).ToList();

            List<ProductTopping> productProductExtras =
                productExtras.Select(pe => new ProductTopping{Topping = pe, ProductId = destination.Id}).ToList();

            return productProductExtras;
        }
    }
    
    public class ConvertFromProductExtra : IMemberValueResolver<Product, ProductResponseModel, ICollection<ProductTopping>?, ICollection<ToppingShallowResponseModel>>
    {
        public ICollection<ToppingShallowResponseModel> Resolve(
            Product source, 
            ProductResponseModel destination, 
            ICollection<ProductTopping>? sourceMember, 
            ICollection<ToppingShallowResponseModel> destMember,
            ResolutionContext context)
        {
            List<Topping> productExtras = (source.ToppingLinks ?? new List<ProductTopping>()).Select(ppe => ppe.Topping).ToList();

            List<ToppingShallowResponseModel> shallowProductExtras = 
                productExtras.Select(pe => context.Mapper.Map<ToppingShallowResponseModel>(pe)).ToList();
            
            return shallowProductExtras;
        }
    }
}
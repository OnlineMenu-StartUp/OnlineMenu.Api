using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineMenu.Api.ViewModel.Product;
using OnlineMenu.Api.ViewModel.ProductExtra;
using OnlineMenu.Application.Dto;
using OnlineMenu.Domain.Models;
// ReSharper disable ClassNeverInstantiated.Global

namespace OnlineMenu.Api.Mapper
{
    public class DefaultMapperProfile: Profile
    {
        public DefaultMapperProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();

            CreateMap<ToppingShallowRequestModel, Topping>();
            CreateMap<Topping, ToppingShallowResponseModel>();
            
            CreateMap<ProductUpdateModel, Product>()
                .ForMember(p => p.ToppingLinks,
                    opt => opt.MapFrom<ToppingIdsToToppingLinks, ICollection<int>?>(src => src.ToppingIds));
            
            CreateMap<ProductCreateModel, Product>()
                .ForMember(p => p.ToppingLinks, 
                    opt => opt.MapFrom<ToppingsToToppingLinks, ICollection<ToppingShallowRequestModel>>(src => src.Toppings));
            
            CreateMap<Product, ProductResponseModel>()
                .ForMember(pr => pr.Toppings,
                    opt => opt.MapFrom<ToppingLinksToToppings, ICollection<ProductTopping>?>(src => src.ToppingLinks))
                .ForMember(pr => pr.CategoryName,
                opt => opt.MapFrom<CategoryToCategoryName, Category?>(src => src.Category));
            
        }
    }

    public class CategoryToCategoryName : IMemberValueResolver<Product, ProductResponseModel, Category?, string?>
    {
        public string? Resolve(
            Product source, 
            ProductResponseModel destination,
            Category? sourceMember,
            string? destMember,
            ResolutionContext context)
        {
            return source.Category?.Name;
        }
    }

    public class ToppingIdsToToppingLinks: IMemberValueResolver<ProductUpdateModel, Product, ICollection<int>?, ICollection<ProductTopping>?>
    {
        public ICollection<ProductTopping>? Resolve(
            ProductUpdateModel source,
            Product destination,
            ICollection<int>? sourceMember,
            ICollection<ProductTopping>? destMember,
            ResolutionContext context)
        {
            return (source.ToppingIds ?? new List<int>())
                .Select(toppingId => new ProductTopping {Product = destination, ToppingId = toppingId}).ToList();
        }
    }

    public class ToppingsToToppingLinks : IMemberValueResolver<ProductCreateModel, Product, ICollection<ToppingShallowRequestModel>, ICollection<ProductTopping>?>
    {

        public ICollection<ProductTopping>? Resolve(
            ProductCreateModel source, 
            Product destination, 
            ICollection<ToppingShallowRequestModel> sourceMember, 
            ICollection<ProductTopping>? destMember,
            ResolutionContext context)
        {
            List<Topping> productExtras =
                source.Toppings.Select(shallowRequestModel => context.Mapper.Map<Topping>(shallowRequestModel)).ToList();

            List<ProductTopping> productProductExtras =
                productExtras.Select(topping => new ProductTopping{Topping = topping, ProductId = destination.Id}).ToList();

            return productProductExtras;
        }
    }
    
    public class ToppingLinksToToppings : IMemberValueResolver<Product, ProductResponseModel, ICollection<ProductTopping>?, ICollection<ToppingShallowResponseModel>>
    {
        public ICollection<ToppingShallowResponseModel> Resolve(
            Product source, 
            ProductResponseModel destination, 
            ICollection<ProductTopping>? sourceMember, 
            ICollection<ToppingShallowResponseModel> destMember,
            ResolutionContext context)
        {
            List<Topping> toppings = (source.ToppingLinks ?? new List<ProductTopping>()).Select(productTopping => productTopping.Topping).ToList();

            List<ToppingShallowResponseModel> shallowToppings = 
                toppings.Select(topping => context.Mapper.Map<ToppingShallowResponseModel>(topping)).ToList();
            
            return shallowToppings;
        }
    }
}
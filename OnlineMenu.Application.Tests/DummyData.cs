using System.Security.Cryptography;
using OnlineMenu.Domain.Models;
using static System.Text.Encoding;

namespace OnlineMenu.Application.Tests
{
    public static class DummyData
    {
        internal const int AdminId = 1;
        internal const string AdminPasswordString = "Password1";
        internal static Admin Admin {
            get
            {
                const string passwordSalt = "adminSalt";
                using var hmac = new HMACSHA512(UTF8.GetBytes(passwordSalt));
                return new Admin
                {
                    // Id = 1,
                    Password = hmac.ComputeHash(UTF8.GetBytes(AdminPasswordString)),
                    PasswordSalt = UTF8.GetBytes(passwordSalt),
                    UserName = "Admin1"
                };
            }
        }

        internal const int CategoryId = 1;

        internal static readonly Category Category =
            new Category
            {
                // Id = 1,
                Name = "Kebab"
            };

        internal static ProductTopping ProductTopping =>
            new ProductTopping
            {
                // Product = Product,
                ProductId = 1,
                // Topping = Topping,
                ToppingId = 1
            };

        internal const int ProductId = 1;

        internal static readonly Product Product =
            new Product
            {
                // Id = 1,
                Name = "Chicken Kebab",
                Description = "Chicken meat, tomato souse",
                Price = 40,
                // ToppingLinks = new List<ProductTopping> {ProductTopping}
            };
        
        internal static readonly Product ProductWithNoToppings =
            new Product
            {
                // Id = 2,
                Name = "Chicken Kebab",
                Description = "Chicken meat, tomato souse",
                Price = 40,
                // ToppingLinks = null
            };

        internal const int ToppingId = 1;

        internal static readonly Topping Topping =
            new Topping
            {
                // Id = 1,
                Name = "Mushrooms",
                Price = 10,
                // ProductLink = new List<ProductTopping> {ProductTopping}
            };
    }
}
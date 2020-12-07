using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;
using Xunit;

namespace OnlineMenu.Application.Tests.ServicesTests.ToppingServiceIntegrationTests
{
    public class ToppingServiceIntegrationTests: IClassFixture<ToppingsSharedDatabaseFixture>
    {
        public ToppingServiceIntegrationTests(ToppingsSharedDatabaseFixture databaseFixture) => DatabaseFixture = databaseFixture;

        private ToppingsSharedDatabaseFixture DatabaseFixture { get; }

        [Fact] 
        public async Task GetAllToppingsAsync_ReturnsAllRelatedEntities_Test()
        {
            // Arrange
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
            
            var service = new ToppingService(context);
            // Act
            var actualResult = (await service.GetAllToppingsAsync()).ToList();

            // Assert
            Assert.Single(actualResult);
            Assert.Equal(DummyData.ToppingId, actualResult.First().Id);
            Assert.Equal(DummyData.ProductId, actualResult.First().ProductLink!.First().ProductId);
            Assert.Equal(DummyData.CategoryId, actualResult.First().ProductLink!.First().Product.CategoryId);
        }

        [Fact]
        public async Task GetToppingByIdAsync_ReturnsAllRelatedEntities_Test()
        {
            // Arrange
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
                
            var service = new ToppingService(context);
            // Act
            var actualResult = await service.GetToppingByIdAsync(DummyData.ToppingId);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(DummyData.ToppingId, actualResult.Id);
            Assert.Equal(DummyData.ProductId, actualResult.ProductLink!.First().ProductId);
            Assert.Equal(DummyData.CategoryId, actualResult.ProductLink!.First().Product.CategoryId);
        }
        
        [Fact]
        public async Task GetToppingByIdAsync_ValueNotFoundException_Test()
        {
            // Arrange
            const int nonexistentToppingId = 99;
            var expectedExceptionMessage = $"Topping with id = {nonexistentToppingId} was not found";

            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
                
            var service = new ToppingService(context);
            // Act & Assert 
            var exception = await Assert.ThrowsAsync<ValueNotFoundException>(() => service.GetToppingByIdAsync(nonexistentToppingId));

            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public async Task GetToppingsByProductIdAsync_Test()
        {
            // Arrange
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
            
            var service = new ToppingService(context);
            // Act
            var actualResult = (await service.GetToppingsByProductIdAsync(DummyData.ProductId)).ToList();

            Assert.Single(actualResult);
            Assert.Equal(DummyData.ToppingId, actualResult.First().Id);
            Assert.Equal(DummyData.ProductId, actualResult.First().ProductLink!.First().ProductId);
            Assert.Equal(DummyData.CategoryId, actualResult.First().ProductLink!.First().Product.CategoryId);
        }
        
        [Fact]
        public async Task GetToppingsByProductIdAsync_BadProductId_ValueNotFoundException_Test()
        {
            // Arrange
            const int nonexistentProductId = 99;
            var expectedExceptionMessage = $"Product with id = {nonexistentProductId} was not found";

            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
                
            var service = new ToppingService(context);
            // Act & Assert 
            var exception = await Assert.ThrowsAsync<ValueNotFoundException>(() => service.GetToppingsByProductIdAsync(nonexistentProductId));

            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public async Task GetToppingsByProductIdAsync_NoToppings_ValueNotFoundException_Test()
        {
            // Arrange
            const int productIdWithNoToppings = 2;
            var expectedExceptionMessage = $"Product with id = {productIdWithNoToppings} doesn't have toppings";

            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
                
            var service = new ToppingService(context);
            // Act & Assert 
            var exception = await Assert.ThrowsAsync<ValueNotFoundException>(() => service.GetToppingsByProductIdAsync(productIdWithNoToppings));

            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async Task CreateToppingAsync_Test()
        {
            // Arrange
            var topping = new Topping
            {
                Name = "Extra meat",
                Price = 20
            };
            const int expectedId = 2;
            
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var service = new ToppingService(context);
                // Act
                var response = await service.CreateToppingAsync(topping);
                
                // Assert
                Assert.Equal(topping.Name, response.Name);
                Assert.Equal(expectedId, response.Id);
            }

            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var item = context.Set<Topping>().Single(e => e.Name == topping.Name);
                
                Assert.Equal(expectedId, item.Id);
                Assert.Equal(topping.Price, item.Price);
            }
        }
        
        [Fact]
        public async Task UpdateToppingAsync_Test()
        {
            // Arrange
            var topping = new Topping
            {
                Name = "Extra meat",
                Price = 20
            };
            const int toppingId = 1;
            
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var service = new ToppingService(context);
                // Act
                var response = await service.UpdateToppingAsync(toppingId, topping);
                
                // Assert
                Assert.Equal(topping.Name, response.Name);
                Assert.Equal(topping.Price, response.Price);
            }

            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var item = context.Set<Topping>().Single(e => e.Id == toppingId);

                Assert.Equal(topping.Name, item.Name);
                Assert.Equal(topping.Price, item.Price);
            }
        }
        
        [Fact]
        public async Task UpdateToppingAsync_NonexistentId_ValueNotFoundException_Test()
        {
            // Arrange
            var topping = new Topping
            {
                Name = "Extra meat",
                Price = 20
            };
            const int nonexistentId = 99;
            var expectedExceptionMessage = $"Topping with id = {nonexistentId} was not found";
            
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
            var service = new ToppingService(context);
            
            // Act & Assert
            var exception =
                await Assert.ThrowsAsync<ValueNotFoundException>(() =>
                    service.UpdateToppingAsync(nonexistentId, topping));

            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public async Task DeleteToppingAsync_Test()
        {
            // Arrange
            const int toppingId = 1;
            
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var service = new ToppingService(context);
                // Act
                var response = await service.DeleteToppingAsync(toppingId);
                
                // Assert
                Assert.Equal(DummyData.ToppingId, response.Id);
                Assert.Equal(DummyData.Topping.Name, response.Name);
            }

            await using (var context = DatabaseFixture.CreateContext(transaction))
            {
                var item = await context.Set<Topping>().SingleOrDefaultAsync(e => e.Id == toppingId);

                Assert.Null(item);
            }
        }
        
        [Fact]
        public async Task DeleteToppingAsync_NonexistentId_ValueNotFoundException_Test()
        {
            // Arrange
            const int nonexistentId = 99;
            var expectedExceptionMessage = $"Topping with id = {nonexistentId} was not found";
            
            await using var transaction = await DatabaseFixture.Connection.BeginTransactionAsync();
            await using var context = DatabaseFixture.CreateContext(transaction);
            var service = new ToppingService(context);
            // Act & Assert
            var exception = 
                await Assert.ThrowsAsync<ValueNotFoundException>(() => 
                    service.DeleteToppingAsync(nonexistentId));
                
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
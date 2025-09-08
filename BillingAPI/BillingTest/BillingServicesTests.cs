using Domain.Entities;
using FluentAssertions;
using Moq;
using Repository.Interfaces;
using Services.Interfaces;
using Services.ServicesClasses;

namespace BillingTest
{
    public class BillingServicesTests
    {
        private readonly Mock<IRepository<Billing>> _repositoryMock;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly BillingServices _billingServices;

        public BillingServicesTests()
        {
            _repositoryMock = new Mock<IRepository<Billing>>();
            _customerServiceMock = new Mock<ICustomerService>();
            _productServiceMock = new Mock<IProductService>();

            _billingServices = new BillingServices(
                _repositoryMock.Object,
                _customerServiceMock.Object,
                _productServiceMock.Object);
        }

        [Fact]
        public async Task CreateBilling_WithMultipleProducts_ShouldCalculateTotalCorrectly()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var product1Id = Guid.NewGuid();
            var product2Id = Guid.NewGuid();

            var customer = new Customer { Id = customerId, Name = "Test Customer", Email = "customer@teste.com", Address = "Street 123" };
            var product1 = new Product { Id = product1Id, NomeDoProduto = "Product 1"};
            var product2 = new Product { Id = product2Id, NomeDoProduto = "Product 2"};

            _customerServiceMock.Setup(x => x.GetCustomerById(customerId))
                .ReturnsAsync(customer);

            _productServiceMock.Setup(x => x.GetProductById(product1Id))
                .ReturnsAsync(product1);

            _productServiceMock.Setup(x => x.GetProductById(product2Id))
                .ReturnsAsync(product2);

            var billing = new Billing
            {
                CustomerId = customerId,
                BillingLines = new List<BillingLine>
                {
                    new BillingLine { ProductId = product1Id, Quantity = 3, UnitPrice = 50m },
                    new BillingLine { ProductId = product2Id, Quantity = 2, UnitPrice = 75m }
                }
            };

            // Act
            var result = await _billingServices.CreateBilling(billing);

            // Assert
            result.First().TotalAmount.Should().Be(300m); // (3*50) + (2*75) = 150 + 150 = 300
            result.First().BillingLines.Should().HaveCount(2);
        }


        [Fact]
        public async Task CreateBilling_WithValidData_ShouldCreateBilling()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var customer = new Customer { Id = customerId, Name = "Test Customer", Email="customer@teste.com", Address = "Street 123" };
            var product = new Product { Id = productId, NomeDoProduto = "Test Product" };

            _customerServiceMock.Setup(x => x.GetCustomerById(customerId))
                .ReturnsAsync(customer);

            _productServiceMock.Setup(x => x.GetProductById(productId))
                .ReturnsAsync(product);

            var billing = new Billing
            {
                CustomerId = customerId,
                BillingLines = new List<BillingLine>
                {
                    new BillingLine { ProductId = productId, Quantity = 2, UnitPrice = 100 }
                }
            };

            // Act
            var result = await _billingServices.CreateBilling(billing);

            // Assert
            result.Should().NotBeNull();
            result.First().TotalAmount.Should().Be(200);
            _repositoryMock.Verify(x => x.CreateBilling(It.IsAny<Billing>()), Times.Once);
        }

        [Fact]
        public async Task CreateBilling_WithNonExistentCustomer_ShouldThrowException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var billing = new Billing { CustomerId = customerId };

            _customerServiceMock.Setup(x => x.GetCustomerById(customerId))
                .ReturnsAsync((Customer?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _billingServices.CreateBilling(billing));
        }

        [Fact]
        public async Task DeleteBilling_WithValidId_ShouldCallRepository()
        {
            // Arrange
            var validId = 1;

            // Act
            var result = await _billingServices.DeleteBilling(validId);

            // Assert
            result.Should().Be("Billing was deleted.");
            _repositoryMock.Verify(x => x.DeleteBilling(validId), Times.Once);
        }

    }
}

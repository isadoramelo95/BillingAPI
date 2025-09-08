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
    }
}


using AutoMapper;
using OrderService.Repository;
using System.Collections.Generic;

namespace OrderServiceAPI.Test.Controller.Carts
{
    public class OrderServiceCartControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        // _sut : System Under Test
        private readonly CartController _sut;
        private readonly ILoggerManager _loggerMock;
        private readonly IMapper _mapperMock;

        public OrderServiceCartControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            // Initialize _logger with the mock object
            var loggerMock = new Mock<ILoggerManager>();
            _loggerMock = loggerMock.Object;

            // Initialize _logger with the mock object
            //var mapperMock = new Mock<IMapper>();
            //_mapperMock = mapperMock.Object;

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<CartAddDto, Cart>(It.IsAny<CartAddDto>()))
                .Returns((CartAddDto source) => new Cart
                {
                    // Initialize properties as needed for CartAddDto to Cart mapping
                    // Assuming Cart has a property CartCustomization of type List<CartCustomization>
                    CartCustomization = source.CartCustomization?
                        .Select(cc => new CartCustomization
                        {
                            CustomizationID = cc.CustomizationID,
                            Price = cc.Price,
                            Quantity = cc.Quantity
                        }).ToList()
                });

            // Setup for CartUpdateDto to Cart mapping
            mapperMock.Setup(m => m.Map<CartUpdateDto, Cart>(It.IsAny<CartUpdateDto>()))
                .Returns(new Cart());

            _mapperMock = mapperMock.Object;

            _sut = new CartController(_serviceMock.Object, _loggerMock, _mapperMock);
        }

        [Fact]
        public void GetCart_ShouldRetunData_WhenCartExists()
        {
            // Arrange
            long customerId = _fixture.Create<long>();
            var mockCartItems = _fixture.Create<List<Cart>>();
            foreach (var item in mockCartItems)
            {
                item.CustomerID = customerId;
            }
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.CustomerID == customerId, It.IsAny<string[]>()))
                           .Returns(mockCartItems.AsEnumerable());

            // Act
            var result = _sut.GetCart(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Cart>>>();
            _serviceMock.Verify(x => x.Cart.FindByCondition(c => c.CustomerID == customerId, It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public void GetCart_ShouldReturnNotFound_WhenCartDoesNotExist()
        {
            // Arrange
            long customerId = _fixture.Create<long>();
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.CustomerID == customerId, It.IsAny<string[]>()))
               .Returns((IEnumerable<Cart>)null);

            // Act
            var result = _sut.GetCart(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            _serviceMock.Verify(x => x.Cart.FindByCondition(c => c.CustomerID == customerId, It.IsAny<string[]>()), Times.Once());
        }

        // =============================
        // Get Cart Item By Test Section
        // =============================
        [Fact]
        public void GetCartItem_ShouldRetunData_WhenCartItemExists()
        {
            // Arrange
            var cartMock = _fixture.Create<Cart>();
            Guid cartId = _fixture.Create<Guid>();

            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>())).Returns(cartMock);

            // Act
            var result = _sut.GetCartItem(cartId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Should().BeAssignableTo<ActionResult<Cart>>();
            _serviceMock.Verify(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>()), Times.Once());
        }


        [Fact]
        public void GetCartItem_ShouldRetunNotFound_WhenCartItemDoesNotExists()
        {
            // Arrange
            Guid cartId = _fixture.Create<Guid>();
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>())).Returns((Cart)null);

            // Act
            var result = _sut.GetCartItem(cartId);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            _serviceMock.Verify(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>()), Times.Once());
        }

        // =============================
        // Post Test Section
        // =============================

        [Fact]
        public void PostCartItem_ShouldReturnCreated_WhenValidRequest()
        {
            // Arrange
            var request = _fixture.Create<CartAddDto>();
            var cart = _mapperMock.Map<CartAddDto, Cart>(request);
            _serviceMock.Setup(x => x.Cart.Create(cart)).Verifiable();

            // Act
            var result = _sut.PostCartItem(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Cart>>();
            result.Result.Should().BeAssignableTo<CreatedAtActionResult>();
            _serviceMock.Verify(x => x.Cart.Create(cart), Times.Never());
            _serviceMock.Verify(x => x.Complete(), Times.Once());
            _serviceMock.Verify(x => x.Commit(), Times.Once());
        }


        [Fact]
        public void PostCartItem_ShouldRetunNotFound_WhenInvalidRequest()
        {
            // Arrange
            var request = _fixture.Create<CartAddDto>();
            var cart = _mapperMock.Map<CartAddDto, Cart>(request);
            _sut.ModelState.AddModelError("Cart", "The cart must be valid");
            var response = _fixture.Create<Cart>();
            _serviceMock.Setup(x => x.Cart.Create(cart));

            // Act
            var result = _sut.PostCartItem(request);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            _serviceMock.Verify(x => x.Cart.Create(cart), Times.Never());
        }

        [Fact]
        public void PostCartItem_ShouldReturnConflict_WhenConcurrencyExceptionOccurs()
        {
            // Arrange
            var request = _fixture.Create<CartAddDto>();
            //var marResult = _mapperMock.Map<CartAddDto, Cart>(request);
            _serviceMock.Setup(x => x.Cart.Create(It.IsAny<Cart>()))
                        .Throws(new DbUpdateConcurrencyException());

            // Act
            var result = _sut.PostCartItem(request);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<ConflictObjectResult>();
            _serviceMock.Verify(x => x.Cart.Create(It.IsAny<Cart>()), Times.Once());
        }

        [Fact]
        public void PostCartItem_ShouldReturnUnprocessableEntity_WhenItemAlreadyExists()
        {
            // Arrange
            var request = _fixture.Create<CartAddDto>();
            //var marResult = _mapperMock.Map<CartAddDto, Cart>(request);
            _serviceMock.Setup(x => x.Cart.CartItemExists(request.CustomerID, request.ProductID))
                        .Returns(true);

            // Act
            var result = _sut.PostCartItem(request);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
        }

        // =============================
        // Put Cart Item Test Section
        // =============================
        [Fact]
        public void PutCartItem_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = new CartUpdateDto(); // Assuming CartUpdateDto is the correct DTO
            _sut.ModelState.AddModelError("error", "some error");

            // Act
            var result = _sut.PutCartItem(cartId, mockCartItem);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public void PutCartItem_ShouldReturnNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = _fixture.Create<CartUpdateDto>(); // Assuming CartUpdateDto is the correct DTO
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>()))
                           .Returns((Cart)null);

            // Act
            var result = _sut.PutCartItem(cartId, mockCartItem);

            // Assert
            result.Should().BeAssignableTo<ActionResult<Cart>>();
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void PutCartItem_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = new CartUpdateDto() { Quantity = 5, CartCustomization = new List<CartCustomization>() }; // Assuming CartUpdateDto is the correct DTO
            var cartItem = new Cart() { Id = cartId, Quantity = 3 }; // Assuming Cart is the correct entity
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>()))
                           .Returns(cartItem);
            _serviceMock.Setup(x => x.Cart.Update(cartItem)).Verifiable();

            // Act
            var result = _sut.PutCartItem(cartId, mockCartItem);

            // Assert
            result.Should().BeAssignableTo<ActionResult<Cart>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            cartItem.Quantity.Should().Be(mockCartItem.Quantity); // Verify that the quantity was updated
        }

        [Fact]
        public void PutCartItem_ShouldReturnConflict_WhenConcurrencyExceptionOccurs()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = new CartUpdateDto();
            var cartItem = new Cart() { Id = cartId };
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.Id == cartId, It.IsAny<string>()))
                           .Returns(cartItem);
            _serviceMock.Setup(x => x.Cart.Update(cartItem))
                           .Throws(new DbUpdateConcurrencyException());

            // Act
            var result = _sut.PutCartItem(cartId, mockCartItem);

            // Assert
            //result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Cart>>();
            result.Result.Should().BeAssignableTo<ConflictObjectResult>();
        }

        // =============================
        // Delete Test Section
        // =============================
        [Fact]
        public void DeleteCartItem_ShouldReturnNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _serviceMock.Setup(x => x.Cart.FindCartItem(cartId)).Returns((Cart)null);

            // Act
            var result = _sut.DeleteCartItem(cartId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
            _serviceMock.Verify(x => x.Cart.FindCartItem(cartId), Times.Once());
        }

        [Fact]
        public void DeleteCartItem_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = new Cart { Id = cartId };
            _serviceMock.Setup(x => x.Cart.FindCartItem(cartId)).Returns(mockCartItem);
            _serviceMock.Setup(x => x.CartCustomization.DeleteAll(cartId)).Verifiable();
            _serviceMock.Setup(x => x.Cart.Delete(mockCartItem)).Verifiable();

            // Act
            var result = _sut.DeleteCartItem(cartId);

            // Assert
            result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.Cart.FindCartItem(cartId), Times.Once());
            _serviceMock.Verify(x => x.BeginTransaction(), Times.Once());
            _serviceMock.Verify(x => x.CartCustomization.DeleteAll(cartId), Times.Once());
            _serviceMock.Verify(x => x.Cart.Delete(It.IsAny<Cart>()), Times.Once());
            _serviceMock.Verify(x => x.Complete(), Times.Once());
            _serviceMock.Verify(x => x.Commit(), Times.Once());
        }

        [Fact]
        public void DeleteCartItem_ShouldReturnServerError_WhenConcurrencyExceptionOccurs()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var mockCartItem = new Cart { Id = cartId };
            _serviceMock.Setup(x => x.Cart.FindCartItem(cartId)).Returns(mockCartItem);
            _serviceMock.Setup(x => x.CartCustomization.DeleteAll(cartId)).Throws(new DbUpdateConcurrencyException());

            // Act
            var result = _sut.DeleteCartItem(cartId);

            // Assert
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            _serviceMock.Verify(x => x.Rollback(), Times.Once());

        }

        // =============================
        // Delete All Items Test Section
        // =============================
        [Fact]
        public void DeleteCart_ShouldReturnNotFound_WhenCartDoesNotExist()
        {
            // Arrange
            long customerId = _fixture.Create<long>();
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.CustomerID == customerId))
                           .Returns(Enumerable.Empty<Cart>());

            // Act
            var result = _sut.DeleteCart(customerId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void DeleteCart_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            long customerId = _fixture.Create<long>();
            var mockCartItems = _fixture.Create<List<Cart>>();
            foreach (var item in mockCartItems)
            {
                item.CustomerID = customerId;
                foreach (var customization in item.CartCustomization)
                {
                    customization.CartID = item.Id;
                }
            }
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.CustomerID == customerId))
                           .Returns(mockCartItems);
            _serviceMock.Setup(x => x.CartCustomization.DeleteAll(It.IsAny<Guid>())).Verifiable();
            _serviceMock.Setup(x => x.Cart.DeleteAll(customerId)).Verifiable();

            // Act
            var result = _sut.DeleteCart(customerId);

            // Assert
            result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.Cart.FindByCondition(c => c.CustomerID == customerId), Times.Once());
            _serviceMock.Verify(x => x.BeginTransaction(), Times.Once());
            _serviceMock.Verify(x => x.CartCustomization.DeleteAll(It.IsAny<Guid>()), Times.Exactly(mockCartItems.Count));
            _serviceMock.Verify(x => x.Cart.DeleteAll(customerId), Times.Once());
            _serviceMock.Verify(x => x.Complete(), Times.Once());
            _serviceMock.Verify(x => x.Commit(), Times.Once());
        }

        [Fact]
        public void DeleteCart_ShouldReturnServerError_WhenConcurrencyExceptionOccurs()
        {
            // Arrange
            long customerId = _fixture.Create<long>();
            var mockCartItems = new List<Cart> { new Cart { CustomerID = customerId } };
            _serviceMock.Setup(x => x.Cart.FindByCondition(c => c.CustomerID == customerId))
                           .Returns(mockCartItems.AsEnumerable());

            _serviceMock.Setup(x => x.CartCustomization.DeleteAll(It.IsAny<Guid>()))
                         .Verifiable();
            _serviceMock.Setup(x => x.Cart.DeleteAll(customerId)).Throws(new DbUpdateConcurrencyException());

            // Act
            var result = _sut.DeleteCart(customerId);

            // Assert
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

    }
}
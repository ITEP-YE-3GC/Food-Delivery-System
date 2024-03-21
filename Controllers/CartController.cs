
using Microsoft.EntityFrameworkCore;

namespace OrderService.Controllers
{
    public class CartController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork uniftOfWork, ILoggerManager logger)
        {
            _unitOfWork = uniftOfWork;
            _logger = logger;
        }


        /// <summary>
        /// Get the cuctomer's cart
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        // GET: api/Cart/5
        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<Cart>> GetCart(long customerId)
        {
            var cartItems = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId, new[] { "CartCustomization" });

            if (cartItems == null)
            {
                return NotFound();
            }

            return Ok(cartItems);
        }

        // GET: api/Cart/5
        [HttpGet("{cartId}")]
        public ActionResult<Cart> GetCartItem(Guid cartId)
        {
            var item = _unitOfWork.Cart.FindByCondition(c => c.Id == cartId, "CartCustomization");

            if (item == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {cartId} not found."));
            }

            return item;
        }

        // PUT: api/Cart/3fa85f64-5717-4562-b3fc-2c963f66afa6
        [HttpPut("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public ActionResult<Cart> PutCartItem(Guid cartId, [FromBody] CartUpdateDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided cart data."));
            }

            var item = _unitOfWork.Cart.FindCartItem(customerId, productId);
            if (item == null)
            {
                var cartItem = _unitOfWork.Cart.FindByCondition(c => c.Id == cartId, "CartCustomization");
                if (cartItem == null)
                {
                    return NotFound(new ApiResponse(404, $"Cart item for customer {item.CustomerID} and product {item.ProductID} not found."));
                }

                // Update the main properties of the cart item
                cartItem.Price = item.Price;
                cartItem.Quantity = item.Quantity;

                _unitOfWork.BeginTransaction();
                // Update the sub-details (CartCustomization)
                if (item.CartCustomization != null)
                {
                    // Remove CartCustomization items that are not present in the updated data
                    //cartItem.CartCustomization.RemoveAll(cc => !item.CartCustomization.Any(c => c.CustomizationID == cc.CustomizationID));

                    if (cartItem.CartCustomization != null)
                    {
                        _unitOfWork.CartCustomization.DeleteAll(cartItem.Id);

                        cartItem.CartCustomization.RemoveAll(c => c.CartID == cartId);
                    }
                    foreach (var customization in item.CartCustomization)
                    {
                        cartItem.CartCustomization.Add(customization);

                    }
                }

                
                _unitOfWork.Cart.Update(cartItem);
                _unitOfWork.Complete();
                _unitOfWork.Commit(); // Commit the transaction after successful update

                return Ok(cartItem); // Return the updated item in the response body
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback(); // Rollback the transaction in case of concurrency exception
                _logger.LogError($"Concurrency error while updating cart's item for customer {item.CustomerID}: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while updating cart's item. Please retry the operation."));
            }
            
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public ActionResult<Cart> PostCartItem([FromBody] Cart item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided cart data."));
            }

            try
            {
                if (_unitOfWork.Cart.CartItemExists(item.CustomerID, item.ProductID))
                {
                    return StatusCode(422, new ApiResponse(422, "This item already exists in your cart"));
                }

                _unitOfWork.BeginTransaction();
                _unitOfWork.Cart.Create(item);
                _unitOfWork.Complete();
                _unitOfWork.Commit();

                return CreatedAtAction(nameof(GetCartItem), new { customerId = item.CustomerID, productId = item.ProductID }, item);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"Concurrency error while creating cart's item for customer product {item.ProductID}, erroe: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while creating cart's item. Please retry the operation."));
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCart(long customerId)
        {
            var cart = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);
            if (!cart.Any())
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} not found.")); ;
            }

            try
            {
                _unitOfWork.BeginTransaction();
                
                foreach (var cartItem in cart)
                {
                    if (cartItem.CartCustomization != null)
                    {
                        _unitOfWork.CartCustomization.DeleteAll(cartItem.Id);
                    }
                }

                _unitOfWork.Cart.DeleteAll(customerId);
                _unitOfWork.Complete();
                _unitOfWork.Commit();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"An error occurred while deleting the cart item. erroe : {ex.Message}");
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request."));
            }
        }

        // DELETE: api/Carts/3fa85f64-5717-4562-b3fc-2c963f66afa6
        [HttpDelete("{cartId}")]
        public IActionResult DeleteCartItem(Guid cartId)
        {
            var cartItem = _unitOfWork.Cart.FindCartItem(cartId);
            if (cartItem == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer with Cart ID{cartId} not found."));
            }

            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.CartCustomization.DeleteAll(cartId);
                _unitOfWork.Cart.Delete(cartItem);
                _unitOfWork.Complete();
                _unitOfWork.Commit();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"An error occurred while deleting the cart item. erroe : {ex.Message}");
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request."));
            }
        }


    }
}

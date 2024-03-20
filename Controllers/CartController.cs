
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

        // GET: api/Cart/5
        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<Cart>> GetCart(int customerId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound(new ApiResponse(404, "Cart entity is null."));
            }

            var cartItems = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);

            if (cartItems == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} not found."));
            }

            return Ok(cartItems);
        }

        // GET: api/Cart/5
        [HttpGet("{customerId}/{productId}")]
        public ActionResult<Cart> GetCartItem(int customerId, int productId)
        { 
            if (_unitOfWork.Cart == null)
            {
                return NotFound(new ApiResponse(404, "Cart enity is null."));
            }
            var item = _unitOfWork.Cart.FindCartItem(customerId, productId);

            if (item == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} and product {productId} not found."));
            }

            return Ok(item);
        }

        // PUT: api/Cart/5
        [HttpPut("{customerId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public ActionResult<Cart> PutCartItem(int customerId, [FromBody] CartDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided cart data."));
            }

            try
            {
                if (!_unitOfWork.Cart.CartItemExists(customerId, item.ProductID))
                {
                    return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} and product {item.ProductID} not found."));
                }

                var checkItem = _unitOfWork.Cart.FindCartItem(customerId, item.ProductID);
                checkItem.Quantity = item.Quantity;

                _unitOfWork.Cart.Update(checkItem);
                _unitOfWork.Complete();

                return Ok(checkItem); // Return the updated item in the response body
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error while updating cart's item for customer {customerId}: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while updating cart's item. Please retry the operation."));
            }
        }


        // POST: api/Cart
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

                _unitOfWork.Cart.Create(item);
                _unitOfWork.Complete();

                return CreatedAtAction(nameof(GetCartItem), new { customerId = item.CustomerID, productId = item.ProductID }, item);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error while creating cart's item for customer {item.ProductID}: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while creating cart's item. Please retry the operation."));
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCartItems(int customerId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound(new ApiResponse(404, "Cart entity is null."));
            }

            var cart = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);
            if (cart == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} not found.")); ;
            }

            _unitOfWork.Cart.DeleteAll(customerId);
            _unitOfWork.Complete();

            return NoContent();
        }

        // DELETE: api/Carts/5
        [HttpDelete("{customerId}/{productId}")]
        public IActionResult DeleteCartItem(int customerId, int productId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound(new ApiResponse(404, "Cart entity is null."));
            }

            var cartItem = _unitOfWork.Cart.FindCartItem(customerId, productId);
            if (cartItem == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} and product {productId} not found."));
            }

            _unitOfWork.Cart.Delete(cartItem);
            _unitOfWork.Complete();

            return NoContent();
        }

    }
}

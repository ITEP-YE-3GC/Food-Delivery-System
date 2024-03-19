
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
        public async Task<ActionResult<User>> GetCart(int customerId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound();
            }
            var cartItems = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);

            if (cartItems == null)
            {
                return NotFound();
            }

            return Ok(cartItems);
        }

        [HttpGet("{customerId}/{productId}")]
        public async Task<ActionResult<Cart>> GetCartItem(int customerId, int productId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.Cart.FindCartItem(customerId, productId);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Cart/5
        [HttpPut("{customerId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutCartItem(int customerId, int productId, [FromBody] CartDTO cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided cart data."));
            }

            var item = _unitOfWork.Cart.FindCartItem(customerId, productId);
            if (item == null)
            {
                return NotFound(new ApiResponse(404, $"Cart item for customer {customerId} and product {productId} not found."));
            }

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error while updating cart item for customer {customerId} and product {productId}: {ex.Message}");
                return Conflict(new ApiResponse(409, "A conflict occurred while updating the cart item. Please retry the operation."));
            }

            return NoContent();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCartItem([FromBody] Cart cart)
        {
            if (_unitOfWork.Cart == null)
            {
                return Problem("Entity set 'ApplicationContext.Carts' is null.");
            }
            _unitOfWork.Cart.Create(cart);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetCartItem), new { customerId = cart.CustomerID, productId = cart.ProductID }, cart);
        }

        // DELETE: api/Users/5
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCartItems(int customerId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound();
            }
            var cart = _unitOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);
            if (cart == null)
            {
                return NotFound();
            }

            _unitOfWork.Cart.DeleteAll(customerId);
            _unitOfWork.Complete();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{customerId}/{productId}")]
        public async Task<IActionResult> DeleteCartItem(int customerId, int productId)
        {
            if (_unitOfWork.Cart == null)
            {
                return NotFound();
            }
            var cart = _unitOfWork.Cart.FindCartItem(customerId, productId);
            if (cart == null)
            {
                return NotFound();
            }

            _unitOfWork.Cart.DeleteAll(customerId);
            _unitOfWork.Complete();

            return NoContent();
        }


    }
}

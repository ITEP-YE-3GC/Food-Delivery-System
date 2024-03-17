using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Controllers.Base;
using OrderService.Entities.Model;
using OrderService.Entities.Model.DTOs;
using static OrderService.Entities.Model.DTOs.CartAutoMapper;

namespace OrderService.Controllers
{
    public class CartController : BaseController
    {
        private readonly IUnitOfWork _uniftOfWork;
        private ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork uniftOfWork, ILoggerManager logger)
        {
            _uniftOfWork = uniftOfWork;
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
            if (_uniftOfWork.Cart == null)
            {
                return NotFound();
            }
            var cartItems = _uniftOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);

            if (cartItems == null)
            {
                return NotFound();
            }

            return Ok(cartItems);
        }

        [HttpGet("{customerId}/{productId}")]
        public async Task<ActionResult<Cart>> GetCartItem(int customerId, int productId)
        {
            if (_uniftOfWork.Cart == null)
            {
                return NotFound();
            }
            var item = _uniftOfWork.Cart.FindCartItem(customerId, productId);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Cart/5
        [HttpPut("{customerId}/{productId}")]
        public async Task<IActionResult> PutCartItem(int customerId, int productId, [FromBody] CartDTO cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _uniftOfWork.Cart.FindCartItem(customerId, productId);
            if (item == null)
            {
                return NotFound();
            }

            _mapper.Map(cart, item);

            try
            {
                _uniftOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error: {ex.Message}");
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return NoContent();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCartItem([FromBody] Cart cart)
        {
            if (_uniftOfWork.Cart == null)
            {
                return Problem("Entity set 'ApplicationContext.Carts' is null.");
            }
            _uniftOfWork.Cart.Create(cart);
            _uniftOfWork.Complete();

            return CreatedAtAction(nameof(GetCartItem), new { customerId = cart.CustomerID, productId = cart.ProductID }, cart);
        }

        // DELETE: api/Users/5
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCartItems(int customerId)
        {
            if (_uniftOfWork.Cart == null)
            {
                return NotFound();
            }
            var cart = _uniftOfWork.Cart.FindByCondition(c => c.CustomerID == customerId);
            if (cart == null)
            {
                return NotFound();
            }

            _uniftOfWork.Cart.DeleteAll(customerId);
            _uniftOfWork.Complete();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{customerId}/{productId}")]
        public async Task<IActionResult> DeleteCartItem(int customerId, int productId)
        {
            if (_uniftOfWork.Cart == null)
            {
                return NotFound();
            }
            var cart = _uniftOfWork.Cart.FindCartItem(customerId, productId);
            if (cart == null)
            {
                return NotFound();
            }

            _uniftOfWork.Cart.DeleteAll(customerId);
            _uniftOfWork.Complete();

            return NoContent();
        }


    }
}

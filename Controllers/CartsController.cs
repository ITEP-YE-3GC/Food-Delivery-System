using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Entities.Model;
using OrderService.Entities.Model.DTOs.RequestDTO;

namespace OrderService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _uniftOfWork;
        private ILoggerManager _logger;
        private readonly IMapper _mapper;


        public CartsController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _uniftOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carts>>> GetCarts()
        {

            if (_uniftOfWork.Carts == null)
            {
                return NotFound();
            }
            var allCarts = _uniftOfWork.Carts.GetAll();
            return Ok(allCarts);


        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carts>> GetCart(int id)
        {
            if (_uniftOfWork.Carts == null)
            {
                return NotFound();
            }
            var cart = _uniftOfWork.Carts.GetById(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, CartAddDTO cart)
        {
            
            var existingCart = _uniftOfWork.Carts.GetById(id);

            if (existingCart == null)
            {
                return NotFound();
            }

            _mapper.Map(cart, existingCart);

            try
            {
                _uniftOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carts>> PostCarts([FromBody] CartAddDTO cart)
        {
           
            if (_uniftOfWork.Carts == null)
            {
                return Problem("Entity set 'ApplicationContext.Carts'  is null.");
            }
            var curr = _mapper.Map<Carts>(cart);
            _uniftOfWork.Carts.Create(curr);
            _uniftOfWork.Complete();

            return CreatedAtAction("GetCart", new { id = curr.Seq }, curr);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarts(int id)
        {
            if (_uniftOfWork.Carts == null)
            {
                return NotFound();
            }
            var Cart = _uniftOfWork.Carts.GetById(id);
            if (Cart == null)
            {
                return NotFound();
            }

            _uniftOfWork.Carts.Delete(Cart);
            _uniftOfWork.Complete();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return (_uniftOfWork.Carts.GetAll()?.Any(e => e.Seq == id)).GetValueOrDefault();
        }
    }
}

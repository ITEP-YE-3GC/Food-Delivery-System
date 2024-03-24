
using OrderService.Entities.Model;

namespace OrderService.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Getorders()
        {
            var allOrders = _unitOfWork.Order.GetAll("OrderDetails");

            if (allOrders == null)
            {
                return NotFound(new ApiResponse(404, $"No Orders has been found."));
            }

            return Ok(allOrders);
        }

        // GET: api/Order/5
        [HttpGet("orderId")]
        public async Task<ActionResult<Order>> GetOrder(Guid orderId)
        {
            var order = _unitOfWork.Order.FindByCondition(o => o.Id == orderId, "OrderDetails");

            if (order == null)
            {
                return NotFound(new ApiResponse(404, $"Order {orderId} not found."));
            }

            return Ok(order);
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("orderId")]
        public async Task<IActionResult> PutOrder(Guid orderId, Order order)
        {
            if ( orderId != order.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Order.Update(order);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody]Order order)
        {
            if (_unitOfWork.Order == null)
            {
                return Problem("Entity set 'ApplicationContext.Order'  is null.");
            }
            _unitOfWork.Order.Create(order);
            _unitOfWork.Complete();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }


        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_unitOfWork.Order == null)
            {
                return NotFound();
            }
            var order = _unitOfWork.Order.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

         

            // Delete associated OrderDetails
            var orderDetails = _unitOfWork.OrderDetails.GetById(id);
            if (orderDetails != null)
            {
                
                    _unitOfWork.OrderDetails.Delete(orderDetails);
                
            }
            

            _unitOfWork.Order.Delete(order);
            _unitOfWork.Complete();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_unitOfWork.Order.GetAll()?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}

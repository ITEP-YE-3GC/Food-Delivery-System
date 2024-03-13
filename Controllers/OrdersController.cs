using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Entities.Model;

namespace OrderService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdresController : ControllerBase
    {
        private readonly IUnitOfWork _uniftOfWork;
        private ILoggerManager _logger;

        public OrdresController(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _uniftOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Getorders()
        {

            if (_uniftOfWork.Order == null)
            {
                return NotFound();
            }
            var allOrders = _uniftOfWork.Order.GetAll();

            // Retrieve OrderDetails for each Order
            foreach (var order in allOrders)
            {
                //order.OrderDetails = _uniftOfWork.OrderDetails.GetById(order.OrderID).ToList();
                order.OrderDetails = new List<OrderDetails>();
                var orderDetail = _uniftOfWork.OrderDetails.GetById(order.OrderID);
                if (orderDetail != null)
                {
                    order.OrderDetails.Add(orderDetail);
                }
            }
            _logger.LogInfo($"Returned  Orders from database.");
            return Ok(allOrders);


        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            if (_uniftOfWork.Order == null)
            {
                return NotFound();
            }
            var order = _uniftOfWork.Order.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Orders order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _uniftOfWork.Order.Update(order);

            try
            {
                _uniftOfWork.Complete();
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrder([FromBody]Orders order)
        {
            if (_uniftOfWork.Order == null)
            {
                return Problem("Entity set 'ApplicationContext.Orders'  is null.");
            }
            _uniftOfWork.Order.Create(order);
            _uniftOfWork.Complete();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_uniftOfWork.Order == null)
            {
                return NotFound();
            }
            var order = _uniftOfWork.Order.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

         

            // Delete associated OrderDetails
            var orderDetails = _uniftOfWork.OrderDetails.GetById(id);
            if (orderDetails != null)
            {
                
                    _uniftOfWork.OrderDetails.Delete(orderDetails);
                
            }
            

            _uniftOfWork.Order.Delete(order);
            _uniftOfWork.Complete();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_uniftOfWork.Order.GetAll()?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}

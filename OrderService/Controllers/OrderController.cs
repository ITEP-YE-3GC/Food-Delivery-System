
namespace OrderService.Controllers
{
    public class OrderController : BaseController
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
        public ActionResult<IEnumerable<Order>> GetOrder()
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
        public ActionResult<Order> GetOrder(Guid orderId)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutOrder(Guid orderId, [FromBody] OrderUpdateDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided order data."));
            }

            try
            {
                var orderResult = _unitOfWork.Order.FindByCondition(o => o.Id == orderId, "OrderDetails");
                if (orderResult == null)
                {
                    return NotFound(new ApiResponse(404, $"Order {orderId} not found."));
                }

                // Update the main properties of the Order
                orderResult.DriverID = IsZeroOrNull(order.DriverID) ? orderResult.DriverID : order.DriverID;
                orderResult.OrderStatusID = IsZeroOrNull(order.StatusID) ? orderResult.OrderStatusID : order.StatusID;

                _unitOfWork.BeginTransaction();

                _unitOfWork.Order.Update(orderResult);
                _unitOfWork.Complete();
                _unitOfWork.Commit();

                return Ok(orderResult);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"Concurrency error while updating order {orderId}: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while updating order. Please retry the operation."));
            }
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public ActionResult<Order> PostOrder([FromBody] OrderAddDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Validation failed for the provided order data."));
            }

            try
            {

                var orderResult = _mapper.Map<OrderAddDto, Order>(order);

                _unitOfWork.BeginTransaction();
                _unitOfWork.Order.Create(orderResult);
                _unitOfWork.Complete();
                _unitOfWork.Commit();

                return CreatedAtAction(nameof(GetOrder), new { orderId = orderResult.Id }, orderResult);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError($"Concurrency error while creating order for customer {order.CustomerID}, erroe: {ex.Message}");
                return Conflict(new ApiResponse(409, "Concurrency error while creating order. Please retry the operation."));
            }
        }

        private Func<int?, bool> IsZeroOrNull = value => value?.Equals(0) ?? false;
    }
}

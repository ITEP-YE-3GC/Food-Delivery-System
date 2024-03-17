using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Entities.Model;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [ApiConventionType(typeof(DefaultApiConventions))]


    public class CartController : ControllerBase

    {

        private readonly IUnitOfWork _uniftOfWork;
        

        public CartController(IUnitOfWork unitOfWork)
        {
            _uniftOfWork = unitOfWork;
        }
     /*   public void EditItem(int id)

        {
            var item = _uniftOfWork.Carts.GetById(id);
            if (item != null)
            {
                _uniftOfWork.Carts.Update(item);
                _uniftOfWork.Complete();
            }
        }*/

        
       

       
        [HttpPut ]
        public async Task<IActionResult> EditItem(int id, Orders order)
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
                    EditItem(id,order);
                }
            }

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_uniftOfWork.Order.GetAll()?.Any(e =>e.OrderID == id)).GetValueOrDefault();
        }
    }
}

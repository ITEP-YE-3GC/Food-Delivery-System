using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts;


namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private void DeleteItem(int id)
        {
            var item = _unitOfWork.Carts.GetById(id);

            if (item != null)
            {
                _unitOfWork.Carts.Delete(item);
                _unitOfWork.Complete();
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var item = _unitOfWork.Carts.GetById(id);

            switch (item)
            {
                case null:
                    return NotFound();
                default:
                    if (item.Quantity > 1)
                    {
                        item.Quantity -= 1;
                        _unitOfWork.Carts.Update(item);
                        _unitOfWork.Complete();
                        return Ok(item);
                    }
                    else
                    {
                        DeleteItem(id);
                        return NoContent();
                    }
            }
        }


        [HttpDelete("deleteAllQuantity")]
        public IActionResult DeleteAllQuantity(int id)
        {
            DeleteItem(id);
            return NoContent();
        }

      

    }
}

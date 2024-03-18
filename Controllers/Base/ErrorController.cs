
using Microsoft.AspNetCore.Mvc;
using OrderService.Exceptions.Error;

namespace OrderService.Controllers.Base
{
    [Route("api/errors/{code}")]
    /**
    * Error : Why this error in swagger(Fetch errorundefined /swagger/v1/swagger.json)
    * Why   : Because this API controller dos not have a API functionality [GET, POST, PUT, DEL, ....]
    * Fix   : [ApiExplorerSettings(IgnoreApi = true)]
    */
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
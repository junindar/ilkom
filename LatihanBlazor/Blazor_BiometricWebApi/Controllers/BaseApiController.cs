using Microsoft.AspNetCore.Mvc;

namespace Blazor_BiometricWebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ObjectResult CreateApiResultForException(string message = "Something went wrong!")
        {
            return Ok(new { Success = false, Message = message });
        }
        protected ObjectResult CreateApiResult(bool success, string message, object data = null)
        {
            return Ok(new { Success = success, Message = message, Result = data });
        }
    }
}

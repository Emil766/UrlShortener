using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace UrlShortener.Controllers
{
    public class BaseController : ControllerBase
    {

        protected ObjectResult StatusCode<TObject>(ServiceResult<TObject> result)
        {
            if (!result.IsSuccessResult)
            {
                return StatusCode(result.StatusCode, result.ExceptionCode);
            }

            return StatusCode(result.StatusCode, result.Model);
        }

        protected ActionResult StatusCode(ServiceResult result)
        {
            if (!result.IsSuccessResult)
            {
                return StatusCode(result.StatusCode, result.ExceptionCode);
            }

            return StatusCode(result.StatusCode);
        }

        protected StatusCodeResult StatusCode(HttpStatusCode code) => StatusCode((int)code);
        protected ObjectResult StatusCode(HttpStatusCode code, object obj) => StatusCode((int)code, obj);
    }
}

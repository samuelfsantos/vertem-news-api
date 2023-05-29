using Microsoft.AspNetCore.Mvc;
using Vertem.News.Infra.Base;
using Vertem.News.Infra.Responses;
using System.Net;

namespace Vertem.News.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult GetCustomResponseSingleData<TResponse>(RequestResult<TResponse> requestResult, string? successInstance = null, string? failInstance = null)
            where TResponse : BaseOutput
        {
            return requestResult.StatusCode switch
            {
                HttpStatusCode.OK => Ok(requestResult.Data),
                HttpStatusCode.Created => CreatedAtAction(successInstance ?? string.Empty, new { requestResult.Data!.Id }, requestResult.Data),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest(GetErrorResponse(failInstance, requestResult.Errors)),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(GetErrorResponse(failInstance, requestResult.Errors)),
                HttpStatusCode.PreconditionFailed => StatusCode(412, GetErrorResponse(failInstance, requestResult.Errors)),
                _ => StatusCode(500, GetErrorResponse(failInstance, requestResult.Errors))
            };
        }

        protected IActionResult GetCustomResponseMultipleData<TResponse>(RequestResult<TResponse> requestResult, string? failInstance = null)
            where TResponse : BaseOutput
        {
            return requestResult.StatusCode switch
            {
                HttpStatusCode.OK => Ok(requestResult.MultipleData),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500, GetErrorResponse(failInstance, requestResult.Errors))
            };
        }

        private static ErrorResponse GetErrorResponse(string? instance, IEnumerable<ErrorModel> errors)
        {
            return new ErrorResponse(instance ?? string.Empty, errors);
        }
    }
}

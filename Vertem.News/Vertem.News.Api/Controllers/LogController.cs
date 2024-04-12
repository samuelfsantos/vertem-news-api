using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vertem.News.Application.Queries;


namespace Vertem.News.Api.Controllers
{
    [Route("api/v1/log")]
    [ApiController]
    public class LogController : BaseController
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogTrace("This is a trace log");
            _logger.LogDebug("This is a debug log");
            _logger.LogInformation("This is an information log");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is an error log");
            _logger.LogCritical("This is a critical log");


            try
            {
                throw new Exception("*** Error ***");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "This is an exception!");
            }

            return Ok();
        }

    }
}

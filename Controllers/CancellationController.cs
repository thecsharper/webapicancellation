using Microsoft.AspNetCore.Mvc;

namespace WebApiCancellation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancellationController : ControllerBase
    {
        private readonly ILogger<CancellationController> _logger;

        public CancellationController(ILogger<CancellationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/cancellationtest")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Long running task..");

            await Task.Run(() =>
            {
                Thread.Sleep(10000);
                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);

            var message = "Finished long running task.";

            _logger.LogInformation(message);

            return message;
        }
    }
}
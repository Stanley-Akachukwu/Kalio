using Kalio.Common;
using Kalio.Domain.Defaults.Weather;
using Kalio.WebAPI.Config;
using Kalio.WebAPI.Securities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Kalio.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        [KalioAuthorize(Permissions.WeatherReadAccess)]
        [ProducesResponseType(typeof(ODataResponse<WeatherViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryWeatherCommand();

            var rsp = await _mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }
        [HttpPost("create")]
        [KalioAuthorize(Permissions.WeatherReadAccess)]
        [ProducesResponseType(typeof(CommandResult<WeatherViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateWeatherCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
    }
}

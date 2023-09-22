
using Kalio.Common;
using Kalio.Entities.Defaults.Weather;
using MediatR;

namespace Kalio.Domain.Defaults.Weather;
public class QueryWeatherCommand : IRequest<CommandResult<IQueryable<WeatherForecast>>>
{

}



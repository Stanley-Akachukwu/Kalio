
using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Defaults.Weather;
public partial class CreateWeatherCommand : CreateCommand, IRequest<CommandResult<WeatherViewModel>>
{

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }

}



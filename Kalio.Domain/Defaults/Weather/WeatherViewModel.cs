namespace Kalio.Domain.Defaults.Weather;

public partial class WeatherViewModel : BaseViewModel
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string Summary { get; set; }

    public string Id { get; set; }

}
 
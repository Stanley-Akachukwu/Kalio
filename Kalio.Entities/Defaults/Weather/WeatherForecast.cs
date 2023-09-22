namespace Kalio.Entities.Defaults.Weather
{
    public class WeatherForecast : BaseEntity<string>
    {
        public WeatherForecast()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }
        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}

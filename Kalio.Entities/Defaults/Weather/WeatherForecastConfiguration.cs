using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kalio.Entities.Defaults.Weather
{

    public partial class WeatherForecastConfiguration : BaseEntityConfiguration<WeatherForecast, string>
    {
        public override void Configure(EntityTypeBuilder<WeatherForecast> entity)
        {
            base.Configure(entity);

            entity.ToTable(nameof(WeatherForecast), DbSchemaConstants.Default);

            entity.Property(s => s.Date)
                .IsRequired();
            entity.Property(s => s.TemperatureC)
               .IsRequired();
            entity.Property(s => s.TemperatureF)
               .IsRequired();
            entity.Property(s => s.Summary)
               .IsRequired();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<WeatherForecast> entity);
    }
}




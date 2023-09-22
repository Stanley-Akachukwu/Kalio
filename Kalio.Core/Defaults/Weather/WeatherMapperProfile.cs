
using AutoMapper;
using Kalio.Domain.Defaults.Weather;
using Kalio.Entities.Defaults.Weather;

namespace Kalio.Core.Defaults.Weather;

public class WeatherMapperProfile : Profile
{

    public WeatherMapperProfile()
    {
        CreateMap<WeatherForecast, WeatherViewModel>().ReverseMap();
        CreateMap<WeatherForecast, CreateWeatherCommand>().ReverseMap();
        CreateMap<WeatherForecast, UpdateWeatherCommand>().ReverseMap();
        CreateMap<WeatherForecast, WeatherForecastMasterView>().ReverseMap();
        CreateMap<WeatherViewModel, WeatherForecastMasterView>().ReverseMap();
        CreateMap<CreateWeatherCommand, WeatherForecastMasterView>().ReverseMap();
        CreateMap<UpdateWeatherCommand, WeatherForecastMasterView>().ReverseMap();
    }
}

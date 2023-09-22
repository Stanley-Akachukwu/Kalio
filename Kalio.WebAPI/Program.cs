using Kalio.Core.Defaults.Weather;
using Kalio.Entities.Defaults.Weather;
using Kalio.WebAPI.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(options =>
    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKalioLibraries(builder.Configuration);
builder.Services.AddKalioDatabaseConnections(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization(builder.Configuration);
builder.Services.AddKalioServices(builder.Configuration);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
    modelBuilder.EntitySet<WeatherForecast>("WeatherForecast");
    return modelBuilder.GetEdmModel();
}
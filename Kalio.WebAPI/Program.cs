using Kalio.Domain.Roles.Claims;
using Kalio.Entities.Defaults.Weather;
using Kalio.WebAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

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
    modelBuilder.EntitySet<IdentityRole>("Roles");
    modelBuilder.EntitySet<IdentityUser>("Users");
    modelBuilder.EntitySet<ClaimViewModel>("Claims");
    return modelBuilder.GetEdmModel();
}
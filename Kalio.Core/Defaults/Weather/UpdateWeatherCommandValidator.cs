
using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Defaults.Weather;
using Kalio.Entities;
using Microsoft.Extensions.Logging;

namespace Kalio.Core.Defaults.Weather;

public partial class UpdateWeatherCommandValidator : AbstractValidator<UpdateWeatherCommand>
{
    private readonly KalioDbContext _dbContext;
    public UpdateWeatherCommandValidator(KalioDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(p => p.Date).NotEmpty().WithMessage("Date required");
        RuleFor(p => p.TemperatureF).NotEmpty().WithMessage("TemperatureF required");
        RuleFor(p => p.TemperatureC).NotEmpty().WithMessage("TemperatureC required");
        RuleFor(p => p.Summary).NotEmpty().WithMessage("Summary required");


        //RuleFor(p => p).Custom((data, context) =>
        //{
        //    var applicationExist = dbContext.SavingsAccountApplications.Where(r => r.Id == data.ApplicationId).Any();
        //    if (!applicationExist)

        //        context.AddFailure(
        //        new ValidationFailure(nameof(data.ApplicationId),
        //        "Saving Application not found", data.ApplicationId));
        //});
    }


}




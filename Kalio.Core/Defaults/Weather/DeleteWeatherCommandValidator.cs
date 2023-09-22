using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Defaults.Weather;
using Kalio.Entities;
using Microsoft.Extensions.Logging;

namespace Kalio.Core.Defaults.Weather;
public partial class DeleteWeatherCommandValidator : AbstractValidator<DeleteWeatherCommand>
{

    private readonly KalioDbContext _dbContext;
    public DeleteWeatherCommandValidator(KalioDbContext appDbContext)
    {

        _dbContext = appDbContext;

        RuleFor(p => p.Id).NotEmpty();  

        //RuleFor(p => p).Custom((data, context) =>
        //{

        //    var checkId = dbContext.SavingsAccounts.Where(r => r.Id == data.Id).Any();
        //    if (!checkId)
        //    {
        //        context.AddFailure(
        //        new ValidationFailure(nameof(data.Id),
        //        "Selected Id does not exist", data.Id));
        //    }

        //    /*
        //    var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
        //    if (checkChild)
        //    {
        //        context.AddFailure(
        //        new ValidationFailure(nameof(data.Id),
        //        "Selected record has dependent records and cannot be deleted", data.Id));

        //    }
        //    */

        //});

    }


}






using AutoMapper;
using Kalio.Common;
using Kalio.Domain.Defaults.Weather;
using Kalio.Entities;
using Kalio.Entities.Defaults.Weather;
using MediatR;

namespace Kalio.Core.Defaults.Weather;


public class WeatherCommandHandler :
      IRequestHandler<QueryWeatherCommand, CommandResult<IQueryable<WeatherForecast>>>,
   IRequestHandler<CreateWeatherCommand, CommandResult<WeatherViewModel>>
   //IRequestHandler<UpdateSavingsAccountCommand, CommandResult<SavingsAccountViewModel>>,
   //IRequestHandler<DeleteSavingsAccountCommand, CommandResult<string>>
{

    private readonly KalioDbContext _dbContext;
    private readonly IMapper _mapper;
    public WeatherCommandHandler(KalioDbContext dbContext, IMapper mapper)
    {

        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<CommandResult<IQueryable<WeatherForecast>>> Handle(QueryWeatherCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<WeatherForecast>>();
      //  var entity = _mapper.Map<WeatherForecast>(request);

        var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray().AsQueryable();


        rsp.Response = weather;

        return rsp;
    }
    public async Task<CommandResult<WeatherViewModel>> Handle(CreateWeatherCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<WeatherViewModel>();
        var entity = _mapper.Map<WeatherForecast>(request);

        var weather = Enumerable.Range(1, 1).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

         entity = weather[0];

       //_dbContext.WeatherForecasts.Add(entity);
       //await _dbContext.SaveChangesAsync();

       rsp.Response = _mapper.Map<WeatherViewModel>(entity);

        return rsp;
    }
    private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };




    //public Task<CommandResult<IQueryable<SavingsAccount>>> Handle(QuerySavingsAccountCommand request, CancellationToken cancellationToken)
    //{

    //    var rsp = new CommandResult<IQueryable<SavingsAccount>>();
    //    rsp.Response = dbContext.SavingsAccounts;

    //    return Task.FromResult(rsp);
    //}



    //public async Task<CommandResult<SavingsAccountViewModel>> Handle(CreateSavingsAccountCommand request, CancellationToken cancellationToken)
    //{

    //    var rsp = new CommandResult<SavingsAccountViewModel>();
    //    var entity = mapper.Map<SavingsAccount>(request);


    //    var accountNo = NHiloHelper.GetNextKey(nameof(SavingsAccount)).ToString();
    //    entity.AccountNo = accountNo;

    //    var product = dbContext.DepositProducts.Where(p => p.Id == request.DepositProductId).FirstOrDefault();

    //    var depositAccount = new LedgerAccount()
    //    {
    //        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
    //        Name = $"Savings - Deposit GL Account ({accountNo})",
    //        Description = "Savings Account",
    //        CurrencyId = product.DefaultCurrencyId,
    //        AccountType = COAType.CONTROL
    //    };
    //    dbContext.LedgerAccounts.Add(depositAccount);
    //    entity.LedgerDepositAccount = depositAccount;

    //    var chargesPayableAccount = new LedgerAccount()
    //    {
    //        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
    //        Name = $"Savings - Charges Payable GL Account ({accountNo})",
    //        Description = "Savings Account",
    //        CurrencyId = product.DefaultCurrencyId,
    //        AccountType = COAType.CONTROL
    //    };
    //    dbContext.LedgerAccounts.Add(chargesPayableAccount);
    //    entity.ChargesPayableAccount = chargesPayableAccount;


    //    var chargesAccruedAccount = new LedgerAccount()
    //    {
    //        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
    //        Name = $"Savings - Charges Accrued GL Account ({accountNo})",
    //        Description = "Savings Account",
    //        CurrencyId = product.DefaultCurrencyId,
    //        AccountType = COAType.CONTROL
    //    };
    //    dbContext.LedgerAccounts.Add(chargesAccruedAccount);
    //    entity.ChargesAccruedAccount = chargesAccruedAccount;



    //    var chargeIncomeAccount = new LedgerAccount()
    //    {
    //        Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString(),
    //        Name = $"Savings - Charge Income GL Account ({accountNo})",
    //        Description = "Savings Account",
    //        CurrencyId = product.DefaultCurrencyId,
    //        AccountType = COAType.CONTROL
    //    };
    //    dbContext.LedgerAccounts.Add(chargeIncomeAccount);
    //    entity.ChargesIncomeAccount = chargeIncomeAccount;


    //    var ChargesWaivedAccount = new LedgerAccount();
    //    ChargesWaivedAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
    //    ChargesWaivedAccount.Name = $"Savings - Charges Waived GL Account ({accountNo})";
    //    ChargesWaivedAccount.Description = "Savings Account";
    //    ChargesWaivedAccount.IsOfficeAccount = true;
    //    ChargesWaivedAccount.AccountType = COAType.CONTROL;
    //    ChargesWaivedAccount.AllowManualEntry = true;
    //    ChargesWaivedAccount.CurrencyId = product.DefaultCurrencyId;
    //    dbContext.LedgerAccounts.Add(ChargesWaivedAccount);
    //    entity.ChargesWaivedAccount = ChargesWaivedAccount;


    //    entity.Caption = $"{product.Name} ({product.Code}) - {entity.AccountNo}";

    //    dbContext.SavingsAccounts.Add(entity);
    //    await dbContext.SaveChangesAsync();

    //    var applicant = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);

    //    var props = new DepositApplicationApproval
    //    {
    //        DepositName = "Savings",
    //        Name = $"{applicant.FirstName} {applicant.LastName}",

    //    };

    //    _ = _emailService.SendEmailAsync(EmailTemplateType.DEPOSIT_APPLICATION_APPROVAL, applicant.PrimaryEmail, props);


    //    rsp.Response = mapper.Map<SavingsAccountViewModel>(entity);

    //    return rsp;
    //}

    //public async Task<CommandResult<SavingsAccountViewModel>> Handle(UpdateSavingsAccountCommand request, CancellationToken cancellationToken)
    //{

    //    var rsp = new CommandResult<SavingsAccountViewModel>();
    //    var entity = await dbContext.SavingsAccounts.FindAsync(request.Id);

    //    mapper.Map(request, entity);

    //    dbContext.SavingsAccounts.Update(entity);
    //    await dbContext.SaveChangesAsync();

    //    rsp.Response = mapper.Map<SavingsAccountViewModel>(entity);

    //    return rsp;
    //}

    //public async Task<CommandResult<string>> Handle(DeleteSavingsAccountCommand request, CancellationToken cancellationToken)
    //{
    //    var rsp = new CommandResult<string>();
    //    var entity = await dbContext.SavingsAccounts.FindAsync(request.Id);

    //    dbContext.SavingsAccounts.Remove(entity);
    //    await dbContext.SaveChangesAsync();

    //    rsp.Response = "Data successfully deleted";

    //    return rsp;
    //}
}




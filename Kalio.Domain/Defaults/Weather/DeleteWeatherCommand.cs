
using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Defaults.Weather;
public partial class DeleteWeatherCommand : DeleteCommand, IRequest<CommandResult<string>>
{

}



using Application.Common.Interfaces;

namespace Infrastructure.RScript;

public class RScriptBuilder : IRScriptBuilder
{
    public Task ProcessRScriptRequestAsync(long Id, CancellationToken cancellationToken)
    {
        //run r script with passed parameter into it
        throw new NotImplementedException();
    }
}

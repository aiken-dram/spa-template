using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IRScriptBuilder
{
    /// <summary>
    /// Processes R script request from message query with provided Id
    /// </summary>
    /// <param name="Id">Id of request from message query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ProcessRScriptRequestAsync(long Id, CancellationToken cancellationToken);
}

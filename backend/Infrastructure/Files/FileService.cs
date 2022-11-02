using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Files;

public partial class FileService : IFileService
{
    private readonly ILogger _logger;
    private readonly Infrastructure.Common.Interfaces.IConfigurationService _configuration;

    public FileService(
        Infrastructure.Common.Interfaces.IConfigurationService configuration,
        ILogger<FileService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
}

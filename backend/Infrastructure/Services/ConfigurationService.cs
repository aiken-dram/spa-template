using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class ConfigurationService :
    Application.Common.Interfaces.IConfigurationService,
    Infrastructure.Common.Interfaces.IConfigurationService
{
    public string RequestStoragePath { get; init; }
    public string DatabaseExportPath { get; init; }
    public string DatabaseExportRemotePath { get; init; }
    public string RScriptPath { get; init; }
    public string HeadersPath { get; init; }
    public int PreviewMaxRows { get; init; }
    public int MessageQueryUserLimit { get; init; }
    public int AuthLock { get; init; }
    public int AuthPeriod { get; init; }
    public int AuthTimeout { get; init; }
    public int AuthBaseTimeout { get; init; }
    public string rPath { get; init; }
    public string rHome { get; init; }
    public string PATH { get; init; }
    public string MQ { get; init; }
    public int MQPort { get; init; }
    public string MQUser { get; init; }
    public string MQPass { get; init; }
    public string MQUri { get; init; }

    private const string _requestStoragePath = "AppSettings:RequestStoragePath";
    private const string _databaseExportPath = "AppSettings:DatabaseExportPath";
    private const string _databaseExportRemotePath = "AppSettings:DatabaseExportRemotePath";
    private const string _RScriptPath = "AppSettings:RScriptPath";
    private const string _headersPath = "AppSettings:HeadersPath";
    private const string _previewMaxRows = "AppSettings:PreviewMaxRows";
    private const string _messageQueryUserLimit = "AppSettings:MessageQueryUserLimit";
    private const string _authLock = "AuthSettings:Lock";
    private const string _authPeriod = "AuthSettings:Period";
    private const string _authTimeout = "AuthSettings:Timeout";
    private const string _authBaseTimeout = "AuthSettings:BaseTimeout";
    private const string _rHome = "RService:rHome";
    private const string _rPath = "RService:rPath";
    private const string _PATH = "RService:PATH";
    private const string _MQ = "MQ:Host";
    private const string _MQPort = "MQ:Port";
    private const string _MQUser = "MQ:Username";
    private const string _MQPass = "MQ:Password";
    private const string _MQUri = "MQ:Uri";

    public ConfigurationService(IConfiguration configuration)
    {
        RequestStoragePath = configuration[_requestStoragePath];
        DatabaseExportPath = configuration[_databaseExportPath];
        DatabaseExportRemotePath = configuration[_databaseExportRemotePath];
        RScriptPath = configuration[_RScriptPath];
        HeadersPath = configuration[_headersPath];
        PreviewMaxRows = Convert.ToInt32(configuration[_previewMaxRows]);
        MessageQueryUserLimit = Convert.ToInt32(configuration[_messageQueryUserLimit]);
        AuthLock = Convert.ToInt32(configuration[_authLock]);
        AuthPeriod = Convert.ToInt32(configuration[_authPeriod]);
        AuthTimeout = Convert.ToInt32(configuration[_authTimeout]);
        AuthBaseTimeout = Convert.ToInt32(configuration[_authBaseTimeout]);
        rHome = configuration[_rHome];
        rPath = configuration[_rPath];
        PATH = configuration[_PATH];
        MQ = configuration[_MQ];
        int port = 0;
        int.TryParse(configuration[_MQPort], out port);
        MQPort = port;
        MQUser = configuration[_MQUser];
        MQPass = configuration[_MQPass];
        MQUri = configuration[_MQUri];
    }
}

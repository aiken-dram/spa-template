using System.Text;

namespace Infrastructure.Files;

public partial class FileService
{
    public async Task<string> ReadRScriptFileAsync(string fname, CancellationToken cancellationToken)
    {
        var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251) ?? Encoding.UTF8;
        var res = await File.ReadAllTextAsync(Path.Combine(_configuration.RScriptPath, fname), enc1251, cancellationToken);
        return res;
    }

    public async Task SaveRScriptFileAsync(string fname, string? content, CancellationToken cancellationToken)
    {
        var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251) ?? Encoding.UTF8;
        await File.WriteAllTextAsync(Path.Combine(_configuration.RScriptPath, fname), content, enc1251, cancellationToken);
    }
}

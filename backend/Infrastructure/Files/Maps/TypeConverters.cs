using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.Files;

public class DateTimeConverter<T> : DefaultTypeConverter
{
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        var d = (DateTime?)value;
        return d.HasValue ? d.Value.ToString("dd.MM.yyyy") : "";
    }
}

using System.Globalization;
using Application.Sample.Queries.GetSampleTable;
using CsvHelper.Configuration;

namespace Infrastructure.Files;

public sealed class SampleTableFileMap : ClassMap<SampleTableDto>
{
    public SampleTableFileMap()
    {
        AutoMap(CultureInfo.GetCultureInfo("ru-RU"));

        Map(m => m.idSample).Ignore();
        Map(m => m.idType).Ignore();
        Map(m => m.idDict).Ignore();
        Map(m => m.text).Name("Text");
        Map(m => m.number).Name("Number");
        Map(m => m.date).Name("Expiration date").TypeConverter<DateTimeConverter<DateTime?>>();
    }
}

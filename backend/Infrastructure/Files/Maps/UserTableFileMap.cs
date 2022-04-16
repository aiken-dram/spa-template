using System.Globalization;
using Application.Account.User.Queries.GetUserTable;
using CsvHelper.Configuration;

namespace Infrastructure.Files;

public sealed class UserTableFileMap : ClassMap<UserTableDto>
{
    public UserTableFileMap()
    {
        AutoMap(CultureInfo.GetCultureInfo("ru-RU"));
        Map(m => m.idUser).Ignore();
        Map(m => m.login).Name("Login");
        Map(m => m.name).Name("Name");
        Map(m => m.desc).Name("Description");
        Map(m => m.passDate).Name("Expiration date").TypeConverter<DateTimeConverter<DateTime?>>();
        Map(m => m.groups).Ignore();
    }
}

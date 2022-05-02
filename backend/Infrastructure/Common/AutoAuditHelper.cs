using System.Reflection;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Application.Helpers;
using Shared.Application.Interfaces;
using Shared.Domain.Attributes;
using Shared.Domain.Enums;
using Shared.Domain.Models;

namespace Infrastructure.Common;

/*
    This is kinda unorganized, i need to somehow place this into single AuditHelper class, buuuuut... dunnno
    maybe leave it here? though the only thing i use is the enums, which kinda should be same across all applications
    so maybe hardcoding em into SPA package is the way to go
    but i also wanna keep AuditService in case i wanna do more stuff here
    and cant unlink it from DbContext, so not sure how to avoid circular reference
    but enough for now, will finish other stuff next
*/

public class AutoAuditHelper
{
    private static AuditEventData EventDataValue(string val)
    {
        var obj = new JsonEventDataValue
        {
            Value = val
        };
        return IAuditService.EventData(obj, (int)eEventDataType.Value);
    }

    private static AuditEventData EventDataFieldValue(string field, string val)
    {
        var obj = new JsonEventDataFieldValue
        {
            Field = field,
            Value = val
        };
        return IAuditService.EventData(obj, (int)eEventDataType.FieldValue);
    }

    private static AuditEventData EventDataFieldOldNew(string field, string oldVal, string newVal)
    {
        var obj = new JsonEventDataFieldOldNew
        {
            Field = field,
            Old = oldVal,
            New = newVal
        };
        return IAuditService.EventData(obj, (int)eEventDataType.FieldOldNew);
    }

    private static string ToString(object? val, AuditAttribute attr)
    {
        switch (val)
        {
            case string s when attr.IsCharBoolean:
                return s == CharBoolean.True ? Shared.Application.Messages.CharBooleanTrue : Shared.Application.Messages.CharBooleanFalse;

            case decimal c when attr.IsCurreny:
                return DisplayHelper.Currency(c);

            case DateTime t when attr.IsTimeStamp:
                return t.ToString(Shared.Application.Messages.TimestampFormat);

            case DateTime d:
                return d.ToString(Shared.Application.Messages.DateTimeFormat);
        }
        return val?.ToString() ?? Shared.Application.Messages.NullValue;
    }

    private static AuditEventData? PropertyCreate<TEntity>(EntityEntry<TEntity> entry, PropertyInfo property)
        where TEntity : class
    {
        //request property
        var attr = AuditHelper.GetAuditAttribute(property);

        //current value
        var currentValue = entry.Property(property.Name).CurrentValue;

        return EventDataFieldValue(property.Name, ToString(currentValue, attr));
    }

    private static AuditEventData? PropertyEdit<TEntity>(EntityEntry<TEntity> entry, PropertyInfo property)
        where TEntity : class
    {
        var attr = AuditHelper.GetAuditAttribute(property);

        var originalValue = entry.Property(property.Name).OriginalValue;
        var currentValue = entry.Property(property.Name).CurrentValue;

        if (!object.Equals(originalValue, currentValue))
            return EventDataFieldOldNew(property.Name, ToString(originalValue, attr), ToString(currentValue, attr));

        return null;
    }

    private static IEnumerable<PropertyInfo> AuditProperties<TEntity>(TEntity entity)
        where TEntity : class
    {
        return entity.GetType().GetProperties()
            .Where(p => Attribute.IsDefined(p, typeof(AuditAttribute)));
    }

    public static AuditEvent Create<TEntity>(EntityEntry<TEntity> entry)
        where TEntity : AuditableEntity
    {
        var e = entry.Entity;
        AuditEvent res = new AuditEvent(e.AuditIdTarget, (int)eEventAction.Create, null, e.AuditTargetName, "AutoAudit");

        //fill all [Audit] values
        foreach (var p in AuditProperties(e))
            res.Add(PropertyCreate(entry, p));

        return res;
    }

    public static AuditEvent Edit<TEntity>(EntityEntry<TEntity> entry)
        where TEntity : AuditableEntity
    {
        var e = entry.Entity;
        AuditEvent res = new AuditEvent(e.AuditIdTarget, (int)eEventAction.Edit, e.AuditTargetId, e.AuditTargetName, "AutoAudit");
        //fill all [Audit] values
        foreach (var p in AuditProperties(e))
            res.Add(PropertyEdit(entry, p));

        return res;
    }

    public static AuditEvent Delete<TEntity>(EntityEntry<TEntity> entry)
        where TEntity : AuditableEntity
    {
        var e = entry.Entity;
        AuditEvent res = new AuditEvent(e.AuditIdTarget, (int)eEventAction.Delete, null, e.AuditTargetName, "AutoAudit");
        return res;
    }
}

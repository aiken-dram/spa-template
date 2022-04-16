using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Filters;

// Source: Clean Architecture example (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
public static class ModelStateHelper
{
    public static string GetModelStateErrors(this ModelStateDictionary modelState)
    {
        IEnumerable<KeyValuePair<string, string[]>> errors = modelState.IsValid
            ? null
            : modelState
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                .Where(m => m.Value.Any());

        string output = "{";

        if (errors != null)
        {
            foreach (KeyValuePair<string, string[]> kvp in errors)
            {
                output += "\"" + kvp.Key.Replace(".", "") + "\":\"" + String.Join(", ", kvp.Value) + "\",";
            }
        }
        output = output.TrimEnd(',');
        output += "}";
        return output;
    }
}

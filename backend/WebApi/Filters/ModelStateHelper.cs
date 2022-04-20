using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Filters;

// Source: hmm dont know even
public static class ModelStateHelper
{
    public static string GetModelStateErrors(this ModelStateDictionary modelState)
    {
        IEnumerable<KeyValuePair<string, string[]?>>? errors = null;
        if (!modelState.IsValid)
        {
            errors = modelState
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray())
                .Where(m => m.Value != null && m.Value.Any());
        }

        string output = "{";

        if (errors != null)
        {
            foreach (KeyValuePair<string, string[]?> kvp in errors)
            {
                output += "\"" + kvp.Key.Replace(".", "") + "\":\"" + String.Join(", ", kvp.Value!) + "\",";
            }
        }
        output = output.TrimEnd(',');
        output += "}";
        return output;
    }
}

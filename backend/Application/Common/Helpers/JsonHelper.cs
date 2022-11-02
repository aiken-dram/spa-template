using Newtonsoft.Json.Linq;

namespace Application.Common.Helpers;

public static class JsonHelper
{
    public static long GetId(string? json)
    {
        if (json == null)
            throw new Exception("JSON must not be null");

        //need to extract RScript id from json and find RScript with that id
        var p = JObject.Parse(json);
        var t = p.SelectToken("$.id");

        if (t == null)
            throw new Exception("id in JSON must exist");

        var _id = t.ToString();

        if (!Int64.TryParse(_id, out long id))
            throw new Exception("id in JSON must be a number");

        return id;
    }

    public static string[] GetArgs(string? json)
    {
        if (json == null)
            throw new Exception("JSON must not be null");

        List<string> res = new List<string>();

        var p = JObject.Parse(json);
        var t = p.SelectToken("$.args");

        if (t == null)
            return res.ToArray();

        JArray arr = (JArray)t;

        foreach (var a in arr)
            res.Add(a.ToString());

        return res.ToArray();
    }
}

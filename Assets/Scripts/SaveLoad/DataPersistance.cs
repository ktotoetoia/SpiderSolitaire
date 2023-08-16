using System.IO;
using UnityJSON;

public class DataPersistance
{
    public static void Save<T>(T obj, string path)
    {
        File.WriteAllText(path, obj.ToJSONString());
    }

    public static T Load<T>(string path)
    {
        return JSON.Deserialize<T>(File.ReadAllText(path));
    }
}
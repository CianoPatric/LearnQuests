using UnityEngine;

public static class JsonConverter
{
    //Сериалицизация
    public static string ToJson<T>(T obj, bool prettyPrint = false)
    {
        return JsonUtility.ToJson(obj, prettyPrint);
    }
    
    //Десериализация
    public static T FromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;

public class Serializer
{
    public const string SAVE_FOLDER = "/Saves";
    public static bool Save(string name, object data)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + SAVE_FOLDER))
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        string path = Application.persistentDataPath + SAVE_FOLDER + '/' + name + ".sprm";
        FileStream file = File.Create(path);
        formatter.Serialize(file, data);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat($"Failed to load, probably wrong path oops {path}");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        selector.AddSurrogate(typeof(Vector3),
            new StreamingContext(StreamingContextStates.All),
            new Vector3SerializationSurrogate());
        selector.AddSurrogate(typeof(Quaternion),
            new StreamingContext(StreamingContextStates.All),
            new QuaternionSerializationSurrogate());

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
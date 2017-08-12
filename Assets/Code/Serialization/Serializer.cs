using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    public static void Save<T>(string filename, T data) where T : class
    {
        using (Stream stream = File.OpenWrite(filename))
        {
            filename = Application.persistentDataPath + @"\Saves\" + filename;
            Stream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            fileStream.Close();
        }
    }

    public static T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(filename))
                {
                    filename = Application.persistentDataPath + @"\Saves\" + filename;
                    Stream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);
                    BinaryFormatter formatter = new BinaryFormatter();
                    fileStream.Close();
                    return formatter.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        return default(T);
    }
}
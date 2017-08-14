using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    public static void Save<T>(string filename, T data) where T : class
    {
        Debug.Log("Saving...");
        using (Stream stream = File.OpenWrite(Application.persistentDataPath + "/" + filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
            Debug.Log("Saving Succesful");
            Debug.Log(Application.persistentDataPath + "/" + filename);
        }
    }

    public static T Load<T>(string filename) where T : class
    {
        Debug.Log("Loading...");
        if (File.Exists(Application.persistentDataPath + "/" + filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(Application.persistentDataPath + "/" + filename))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Debug.Log("Loading Succesful");
                    Debug.Log(Application.persistentDataPath + "/" + filename);
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
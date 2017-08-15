using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour
{
    private static Singleton s_Instance = null;

    public static Singleton instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Singleton)) as Singleton;
            }

            return s_Instance;
        }

        set { }
    }

    private void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    void OnApplicationQuit()
    {
        s_Instance = null;
    }
}

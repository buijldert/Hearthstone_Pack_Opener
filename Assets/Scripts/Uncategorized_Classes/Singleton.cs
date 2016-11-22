using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    void Awake()
    {
        this.tag = Tags.SINGLETONTAG;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
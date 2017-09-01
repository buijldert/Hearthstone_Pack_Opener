using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCollection : MonoBehaviour
{
    public delegate void PoolCollectionAction();
    public static event PoolCollectionAction OnPoolCollection;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartPooling();
        }
    }

    public void StartPooling()
    {
        if(OnPoolCollection != null)
        {
            OnPoolCollection();
        }
    }
}

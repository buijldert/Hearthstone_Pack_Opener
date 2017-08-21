using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPacks : MonoBehaviour
{
    private List<Pack> _collectionPacks;
    private GameObject _packPrefab;

    private void Start()
    {
        _collectionPacks = Serializer.Load<List<Pack>>("packdata.sav");

        if(_collectionPacks != null && _collectionPacks.Count != 0)
        {
            for (int i = 0; i < _collectionPacks.Count; i++)
            {

            }
        }
    }
}

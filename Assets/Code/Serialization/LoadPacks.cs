using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPacks : MonoBehaviour
{
    [SerializeField]
    private List<OnDragBeginPack> _packs;
    private List<Pack> _collectionPacks;
    private GameObject _packPrefab;

    private void Awake()
    {
        _collectionPacks = Serializer.Load<List<Pack>>("packdata.sav");

        if(_collectionPacks != null && _collectionPacks.Count != 0)
        {
            for (int i = 0; i < _collectionPacks.Count; i++)
            {
                for (int j = 0; j < _packs.Count; j++)
                {
                    if(_collectionPacks[i].packExpansion == _packs[j]._packExpansion)
                    {
                        _packs[j]._packCount = _collectionPacks[i].packCount;
                        break;
                    }
                }
            }
        }
    }
}

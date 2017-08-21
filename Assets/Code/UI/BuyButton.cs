using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    private GoldAmount _goldAmount;

    private List<Pack> _collectionPacks;

    private void Start()
    {
        _goldAmount = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<GoldAmount>();
    }

    public void Buy()
    {
        if(_goldAmount.Gold - BuyData.cost >= 0)
        {
            _goldAmount.Gold -= BuyData.cost;
            _collectionPacks = Serializer.Load<List<Pack>>("packdata.sav");
            if(_collectionPacks != null && _collectionPacks.Count == 0)
            {
                _collectionPacks = new List<Pack>();
                Pack boughtPack = new Pack()
                {
                    packExpansion = BuyData.packExpansion,
                    packCount = BuyData.numberOfPacks
                };
                _collectionPacks.Add(boughtPack);
            }
            else
            {
                for (int i = 0; i < _collectionPacks.Count; i++)
                {
                    if (_collectionPacks[i].packExpansion == BuyData.packExpansion)
                    {
                        _collectionPacks[i].packCount += BuyData.numberOfPacks;
                        break;
                    }
                    else if (i == _collectionPacks.Count - 1)
                    {
                        Pack boughtPack = new Pack()
                        {
                            packExpansion = BuyData.packExpansion,
                            packCount = BuyData.numberOfPacks
                        };
                        _collectionPacks.Add(boughtPack);
                        break;
                    }
                }
            }
            Serializer.Save("packdata.sav", _collectionPacks);
            //show purchase succesful screen
        }
        else
        {
            //show purchase failed screen
        }
    }
}

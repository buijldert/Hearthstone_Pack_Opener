using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private GoldAmount _goldAmount;

    private List<Pack> _collectionPacks;

    [FormerlySerializedAs("_purchaseSuccesfulScreen")]
    [SerializeField]
    private Sprite _purchaseSuccesfulSprite;
    [FormerlySerializedAs("_purchaseFailedScreen")]
    [SerializeField]
    private Sprite _purchaseFailedSprite;

    [SerializeField]
    private GameObject _purchaseScreenBG;
    [SerializeField]
    private Image _purchaseScreen;

    private void Start()
    {
        _goldAmount = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<GoldAmount>();
    }

    public void Buy()
    {
        if(_goldAmount._gold - BuyData.cost >= 0)
        {
            _goldAmount.ChangeGold(-BuyData.cost);
            _collectionPacks = Serializer.Load<List<Pack>>("packdata.sav");
            if(_collectionPacks == null || _collectionPacks.Count == 0)
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
            _purchaseScreen.sprite = _purchaseSuccesfulSprite;
            _purchaseScreenBG.SetActive(true);
            //show purchase succesful screen
        }
        else
        {
            _purchaseScreen.sprite = _purchaseFailedSprite;
            _purchaseScreenBG.SetActive(true);
            //show purchase failed screen
        }
    }
}

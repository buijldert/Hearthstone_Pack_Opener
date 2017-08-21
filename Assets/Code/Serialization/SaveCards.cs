using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveCards : MonoBehaviour
{

    private List<Card> _collectionCards;

    private OpenPack _openPack;

	// Use this for initialization
	void Start ()
    {
        _openPack = GameObject.Find("OpenPack").GetComponent<OpenPack>();
        _collectionCards = Serializer.Load<List<Card>>("carddata.sav");
        
        if (_collectionCards == null)
        {
            _collectionCards = new List<Card>();
        }
	}
	public void Save()
    {
        for (int i = 0; i < _openPack._activeCards.Count; i++)
        {
            Image cardImage = _openPack._activeCards[i].GetComponent<Image>();
            if (_collectionCards.Count > 0)
            {
                
                for (int j = 0; j < _collectionCards.Count; j++)
                {
                    if (cardImage.sprite.name == _collectionCards[j].cardName)
                    {
                        _collectionCards[j].cardCount++;
                        break;
                    }
                    else if (j == _collectionCards.Count - 1)
                    {
                        Card openedCard = new Card()
                        {
                            cardName = cardImage.sprite.name,
                            cardRarity = _openPack._activeCards[i].GetComponent<OnCardClick>().cardRarity,
                            cardCount = 1
                        };
                        _collectionCards.Add(openedCard);
                        break;
                    }
                }
            }
            else
            {
                Card openedCard = new Card()
                {
                    cardName = cardImage.sprite.name,
                    cardRarity = _openPack._activeCards[i].GetComponent<OnCardClick>().cardRarity,
                    cardCount = 1
                };
                _collectionCards.Add(openedCard);
            }
        }

        _collectionCards.Sort(delegate (Card i1, Card i2) { return i1.cardName.CompareTo(i2.cardName); });

        Serializer.Save("carddata.sav", _collectionCards);
    }
}

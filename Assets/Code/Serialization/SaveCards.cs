using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveCards : MonoBehaviour
{

    private List<Card> _collectionCards;

	// Use this for initialization
	void Start ()
    {
		if(Serializer.Load<List<Card>>("carddata.sav") != null)
        {
            _collectionCards = Serializer.Load<List<Card>>("carddata.sav");
        }
        else
        {
            _collectionCards = new List<Card>();
            print("made list");
        }
	}
	
	// Update is called once per frame
	public void Save ()
    {
		foreach(GameObject card in GameObject.Find("Pack").GetComponent<OpenPack>()._activeCards)
        {
            if(_collectionCards.Count > 0)
            {
                for (int i = 0; i < _collectionCards.Count; i++)
                {
                    if (_collectionCards[i].cardName == card.GetComponent<Image>().sprite.name)
                    {
                        _collectionCards[i].cardCount += 1;
                        break;
                    }
                    else if (i == _collectionCards.Count - 1)
                    {
                        Card openedCard = new Card();
                        openedCard.cardName = card.GetComponent<Image>().sprite.name;
                        openedCard.cardRarity = card.GetComponent<OnCardClick>().cardRarity;
                        openedCard.cardCount += 1;
                        _collectionCards.Add(openedCard);
                        print("added card");
                    }
                }
            }
            else
            {
                Card openedCard = new Card();
                openedCard.cardName = card.GetComponent<Image>().sprite.name;
                openedCard.cardRarity = card.GetComponent<OnCardClick>().cardRarity;
                openedCard.cardCount += 1;
                _collectionCards.Add(openedCard);
                print("added card");
            }
        }

        _collectionCards.Sort(delegate (Card i1, Card i2) { return i1.cardName.CompareTo(i2.cardName); });

        Serializer.Save("carddata.sav", _collectionCards);
    }

    public void Load()
    {
        _collectionCards = Serializer.Load<List<Card>>("carddata.sav");
        print(_collectionCards.Count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveCards : MonoBehaviour
{
    //A list of all cards in the collection.
    private List<Card> _collectionCards;
    //An instance of the openpack script for 
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

    private void DetermineDustValues(Card card)
    {
        switch (card.cardRarity)
        {
            case Card.Rarity.Common:
                card.cardCraftValue = 20;
                card.cardDisenchantValue = 5;
                break;
            case Card.Rarity.Rare:
                card.cardCraftValue = 100;
                card.cardDisenchantValue = 20;
                break;
            case Card.Rarity.Epic:
                card.cardCraftValue = 400;
                card.cardDisenchantValue = 100;
                break;
            case Card.Rarity.Legendary:
                card.cardCraftValue = 1600;
                card.cardDisenchantValue = 400;
                break;
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
                        Card openedCard = NewCard(cardImage, _openPack._activeCards[i].GetComponent<OnCardClick>()._cardRarity);
                        DetermineDustValues(openedCard);
                        Debug.Log("Rarity: " + openedCard.cardRarity);
                        Debug.Log("Craft value: " + openedCard.cardCraftValue);
                        Debug.Log("Disenchant value: " + openedCard.cardDisenchantValue);
                        _collectionCards.Add(openedCard);
                        break;
                    }
                }
            }
            else
            {
                Card openedCard = NewCard(cardImage, _openPack._activeCards[i].GetComponent<OnCardClick>()._cardRarity);
                _collectionCards.Add(openedCard);
            }
        }

        _collectionCards.Sort(delegate (Card i1, Card i2) { return i1.cardName.CompareTo(i2.cardName); });

        Serializer.Save("carddata.sav", _collectionCards);
    }

    private Card NewCard(Image cardImage, Card.Rarity rarity)
    {
        Card newCard = new Card()
        {
            cardName = cardImage.sprite.name,
            cardRarity = rarity,
        };
        newCard.cardCount += 1;
        return newCard;
    }

}

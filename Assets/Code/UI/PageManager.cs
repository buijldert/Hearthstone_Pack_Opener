using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _collectionCardGameObjects = new List<GameObject>();
    private List<Card> _collectionCards;
    [SerializeField]
    private GameObject _collectionCardPrefab;
    [SerializeField]
    private GameObject _collectionPagePrefab;
    [SerializeField]
    private GameObject _pagesParent;

    private CollectionPageButtons _collectionPageButtons;
    private CardData[] _cardDataArray;

	void Awake ()
    {
        _cardDataArray = GameObject.FindWithTag("CardData").GetComponents<CardData>();
        _collectionPageButtons = gameObject.GetComponent<CollectionPageButtons>();

        _collectionCards = Serializer.Load<List<Card>>("carddata.sav");

        GameObject tempPage = Instantiate(_collectionPagePrefab, _pagesParent.transform);
        _collectionPageButtons._pages.Add(tempPage);

        if (_collectionCards != null)
        {
            for (int i = 0; i < _collectionCards.Count; i++)
            {
                for (int j = 0; j < _cardDataArray.Length; j++)
                {
                    RetrieveAllRarities(_collectionCards[i], j);
                }
            }
            

            for (int i = 0; i < _collectionCardGameObjects.Count; i++)
            {
                _collectionCardGameObjects[i].transform.SetParent(tempPage.transform, false);
                if ((i + 1) % 8 == 0 && i != _collectionCardGameObjects.Count - 1)
                {
                    tempPage = Instantiate(_collectionPagePrefab, _pagesParent.transform);
                    tempPage.transform.SetAsFirstSibling();
                    _collectionPageButtons._pages.Add(tempPage);
                }
            }
        }
    }

    private void RetrieveAllRarities(Card currentCard, int currentExpansion)
    {
        RetrieveCards(currentCard, _cardDataArray[currentExpansion]._commonCards);
        RetrieveCards(currentCard, _cardDataArray[currentExpansion]._rareCards);
        RetrieveCards(currentCard, _cardDataArray[currentExpansion]._epicCards);
        RetrieveCards(currentCard, _cardDataArray[currentExpansion]._legendaryCards);
    }

    private void RetrieveCards(Card currentCard, List<Sprite> cards)
    {
        GameObject tempCard;
        for (int j = 0; j < cards.Count; j++)
        {
            if (currentCard.cardName == cards[j].name)
            {
                tempCard = Instantiate(_collectionCardPrefab);
                tempCard.GetComponent<Image>().sprite = cards[j];
                tempCard.GetComponentInChildren<Text>().text = "x" + currentCard.cardCount;
                _collectionCardGameObjects.Add(tempCard);
                break;
            }
        }
    }
}

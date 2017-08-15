using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    
    //[SerializeField]
    //[FormerlySerializedAs("allCardSprites")]
    //private List<Sprite> _allCardSprites;
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
    private CardData _cardData;

	void Awake ()
    {
        _cardData = GameObject.FindWithTag("CardData").GetComponent<CardData>();
        _collectionPageButtons = gameObject.GetComponent<CollectionPageButtons>();

        _collectionCards = Serializer.Load<List<Card>>("carddata.sav");

        GameObject tempPage = Instantiate(_collectionPagePrefab, _pagesParent.transform);
        _collectionPageButtons._pages.Add(tempPage);

        if (_collectionCards != null)
        {
            for (int i = 0; i < _collectionCards.Count; i++)
            {
                RetrieveCards(_collectionCards[i], _cardData._commonCards);
                RetrieveCards(_collectionCards[i], _cardData._rareCards);
                RetrieveCards(_collectionCards[i], _cardData._epicCards);
                RetrieveCards(_collectionCards[i], _cardData._legendaryCards);

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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Displays all collection pages after deserializing the cards in the collection.
public class PageManager : MonoBehaviour
{
    //THe collection cards gameobjects.
    [SerializeField]
    private List<GameObject> _collectionCardGameObjects = new List<GameObject>();
    //The collection cards themselves.
    private List<Card> _collectionCards;

    //The prefab for a card in the collection.
    [SerializeField]
    private GameObject _collectionCardPrefab;
    //The prefab for a page in the collection.
    [SerializeField]
    private GameObject _collectionPagePrefab;
    //The parent of the pages.
    [SerializeField]
    private GameObject _pagesParent;

    //An instance of the collectionpagebuttons script to add the pages to its pages list.
    private CollectionPageButtons _collectionPageButtons;

    //An array of carddata (one for every expansion).
    private CardData[] _cardDataArray;

    /// <summary>
    /// Deserializes all the cards currently in the player's collection, instatiates them and instantiates pages to fit them.
    /// </summary>
	private void Awake ()
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
                    CheckAllRarities(_collectionCards[i], j);
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

    /// <summary>
    /// Checks the current card against all rarities of the current expansion.
    /// </summary>
    /// <param name="currentCard">The current card.</param>
    /// <param name="currentExpansion">The index of the current expansion</param>
    private void CheckAllRarities(Card currentCard, int currentExpansion)
    {
        SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._commonCards);
        SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._rareCards);
        SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._epicCards);
        SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._legendaryCards);
    }

    /// <summary>
    /// Instantiates the current card and sets its sprite and counter.
    /// </summary>
    /// <param name="currentCard">The current card.</param>
    /// <param name="currentExpansion">The index of the current expansion</param>
    private void SetCurrentCard(Card currentCard, List<Sprite> cards)
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

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
    [SerializeField]
    private Transform examplePage;

    //An instance of the collectionpagebuttons script to add the pages to its pages list.
    private CollectionPageButtons _collectionPageButtons;

    //An array of carddata (one for every expansion).
    private CardData[] _cardDataArray;

    /// <summary>
    /// Deserializes all the cards currently in the player's collection, instatiates them and gets pages from the objectpool to fit them.
    /// </summary>
	private void Awake ()
    {
        PoolCollection.OnPoolCollection += PoolCards;
        _cardDataArray = GameObject.FindWithTag("CardData").GetComponents<CardData>();
        _collectionPageButtons = gameObject.GetComponent<CollectionPageButtons>();

        _collectionCards = Serializer.Load<List<Card>>("carddata.sav");

        GameObject tempPage = ObjectPool.Instance.GetObjectForType("CollectionPage", false);
        tempPage.transform.SetParent(_pagesParent.transform, false);
        tempPage.transform.localScale = Vector3.one;
        tempPage.transform.position = examplePage.position;
        _collectionPageButtons._pages.Add(tempPage);

        if (_collectionCards != null)
        {
            for (int i = 0; i < _collectionCards.Count; i++)
            {
                for (int j = 0; j < _cardDataArray.Length; j++)
                {
                    if(_cardDataArray[j]._packExpansion == _collectionCards[i].cardExpansion)
                    {
                        CheckAllRarities(_collectionCards[i], j);
                        break;
                    }
                }
            }

            for (int i = 0; i < _collectionCardGameObjects.Count; i++)
            {
                _collectionCardGameObjects[i].transform.SetParent(tempPage.transform, false);
                if ((i + 1) % 8 == 0 && i != _collectionCardGameObjects.Count - 1)
                {
                    tempPage = ObjectPool.Instance.GetObjectForType("CollectionPage", false);
                    tempPage.transform.SetParent(_pagesParent.transform, false);
                    tempPage.transform.SetAsFirstSibling();
                    tempPage.transform.localScale = Vector3.one;
                    tempPage.transform.position = examplePage.position;
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
        switch(currentCard.cardRarity)
        {
            case Card.Rarity.Common:
                SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._commonCards);
                break;
            case Card.Rarity.Rare:
                SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._rareCards);
                break;
            case Card.Rarity.Epic:
                SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._epicCards);
                break;
            case Card.Rarity.Legendary:
                SetCurrentCard(currentCard, _cardDataArray[currentExpansion]._legendaryCards);
                break;
            default:
                Debug.LogError("This card has no rarity. Did something go wrong with serialization?");
                break;
        }
    }

    /// <summary>
    /// Gets the current card from the objectpool and sets its sprite and counter.
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
                tempCard = ObjectPool.Instance.GetObjectForType("CollectionCardPrefab", false);
                tempCard.GetComponent<Image>().sprite = cards[j];
                tempCard.transform.localScale = Vector3.one;
                tempCard.GetComponentInChildren<Text>().text = "x" + currentCard.cardCount;
                _collectionCardGameObjects.Add(tempCard);
                break;
            }
        }
    }

    private void PoolCards()
    {
        for (int i = 0; i < _collectionCardGameObjects.Count; i++)
        {
            ObjectPool.Instance.PoolObject(_collectionCardGameObjects[i]);
        }
    }

    private void OnDisable()
    {
        PoolCollection.OnPoolCollection -= PoolCards;
    }
}

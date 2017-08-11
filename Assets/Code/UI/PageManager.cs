using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    
    [SerializeField]
    [FormerlySerializedAs("allCardSprites")]
    private List<Sprite> _allCardSprites;
    [SerializeField]
    private List<GameObject> _collectionCardGameObjects = new List<GameObject>();
    [SerializeField]
    private GameObject _collectionCardPrefab;
    [SerializeField]
    private GameObject _collectionPagePrefab;
    [SerializeField]
    private GameObject _pagesParent;

    private CollectionPageButtons _collectionPageButtons;

	void Awake ()
    {
        _collectionPageButtons = gameObject.GetComponent<CollectionPageButtons>();

        if(Serializer.Load<List<Card>>("carddata.sav") != null)
        {
            List<Card> collectionCards = Serializer.Load<List<Card>>("carddata.sav");
            GameObject tempCard;
            GameObject tempPage = Instantiate(_collectionPagePrefab, _pagesParent.transform);
            _collectionPageButtons._pages.Add(tempPage);

            for (int i = 0; i < collectionCards.Count; i++)
            {
                for (int j = 0; j < _allCardSprites.Count; j++)
                {
                    if (collectionCards[i].cardName == _allCardSprites[j].name)
                    {
                        tempCard = Instantiate(_collectionCardPrefab);
                        tempCard.GetComponent<Image>().sprite = _allCardSprites[j];
                        tempCard.GetComponentInChildren<Text>().text = "x" + collectionCards[i].cardCount;
                        _collectionCardGameObjects.Add(tempCard);
                        break;
                    }
                }
            }


            for (int i = 0; i < _collectionCardGameObjects.Count; i++)
            {
                _collectionCardGameObjects[i].transform.SetParent(tempPage.transform, false);
                if ((i + 1) % 8 == 0)
                {
                    tempPage = Instantiate(_collectionPagePrefab, _pagesParent.transform);
                    tempPage.transform.SetAsFirstSibling();
                    _collectionPageButtons._pages.Add(tempPage);
                }
            }
        }
    }
}

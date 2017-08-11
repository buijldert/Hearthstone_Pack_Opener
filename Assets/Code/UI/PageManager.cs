using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    
    [SerializeField]
    [FormerlySerializedAs("allCardSprites")]
    private List<Sprite> _allCardSprites;

    private List<GameObject> _collectionCardGameObjects = new List<GameObject>();
    [SerializeField]
    private GameObject _collectionCardPrefab;
    [SerializeField]
    private GameObject _collectionPagePrefab;
    [SerializeField]
    private GameObject _canvas;

	void Start ()
    {
		List<Card> collectionCards = Serializer.Load<List<Card>>("carddata.sav");
        GameObject tempCard;
        GameObject tempPage = Instantiate(_collectionPagePrefab, _canvas.transform);

        for (int i = 0; i < collectionCards.Count; i++)
        {
            for (int j = 0; j < _allCardSprites.Count; j++)
            {
                if (collectionCards[i].cardName == _allCardSprites[j].name)
                {
                    tempCard = Instantiate(_collectionCardPrefab);
                    tempCard.GetComponent<Image>().sprite = _allCardSprites[j];
                    tempCard.GetComponentInChildren<Text>().text = "x" + collectionCards[i].cardCount.ToString();
                    _collectionCardGameObjects.Add(tempCard);
                    break;
                }
            }
        }


        for (int i = 1; i < _collectionCardGameObjects.Count; i++)
        {
            if(i%8 == 0)
            {
                tempPage = Instantiate(_collectionPagePrefab, _canvas.transform);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OpenPack : MonoBehaviour {

    [SerializeField]private GameObject _card;
    [SerializeField]private Canvas _canvas;
    [SerializeField]private List<GameObject> _cardSpawnPoints;
    [SerializeField]private GameObject _cardsBackground;
    [SerializeField]private GameObject _doneButton;

    [SerializeField]private int _cardsRevealed = 0;
    public int CardsRevealed
    {
        get
        {
            return _cardsRevealed;
        }
    }

    public List<GameObject> _activeCards = new List<GameObject>();
    
    public delegate void ReturnPackAction();
    public static event ReturnPackAction OnReturnPack;

	// Use this for initialization
	void OnEnable()
    {
        OnDragPack.OnOpenPack += OpenThePack;
        DoneButton.OnClosePack += RemoveCards;
        OnCardClick.OnClickCard += AddRevealedCard;
	}

    void OpenThePack()
    {
        _cardsBackground.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            GameObject card;
            card = Instantiate(_card, _cardSpawnPoints[i].transform.position, _card.transform.rotation) as GameObject;
            card.transform.SetParent(_canvas.transform, false);
            card.transform.position = _cardSpawnPoints[i].transform.position;
            _activeCards.Add(card);
        }
        
        if(OnReturnPack != null)
        {
            OnReturnPack();
        }

    }

    void RemoveCards()
    {
        foreach(GameObject card in _activeCards)
        {
            Destroy(card);
        }
        _activeCards.Clear();
    }

    void AddRevealedCard()
    {
        _cardsRevealed += 1;
        if(_cardsRevealed == 5)
        {
            _cardsRevealed = 0;
            _doneButton.SetActive(true);
        }
    }

    void OnDisable()
    {
        OnDragPack.OnOpenPack -= OpenThePack;
        DoneButton.OnClosePack -= RemoveCards;
        OnCardClick.OnClickCard -= AddRevealedCard;
    }
}
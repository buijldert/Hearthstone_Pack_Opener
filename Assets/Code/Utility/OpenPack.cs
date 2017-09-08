using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OpenPack : MonoBehaviour {

    public delegate void CardsDeterminedAction();
    public static event CardsDeterminedAction OnCardsDetermined;

    [SerializeField]
    private GameObject _card;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private List<GameObject> _cardSpawnPoints;
    [SerializeField]
    private GameObject _cardsBackground;
    [SerializeField]
    private GameObject _doneButton;
    [SerializeField]
    private Transform _packReceiver;

    private int _commonCards;
    private int _cardsRevealed;

    public List<OnCardClick> _activeCards = new List<OnCardClick>();
    public CardData[] _cardDataArray;

    private float _dropChance = 100f;
    private float _drop;

    [SerializeField]
    private List<AudioClip> _turnOverSounds;

#region CARDDATASET
    private Sprite _currentSprite;
    private Pack.Expansion _currentExpansion;
    private Card.Rarity _currentRarity;
    private AudioClip _currentAudioClip;
#endregion

    private void OnEnable()
    {
        OnDragPack.OnOpenPack += OpenThePack;
        OnCardClick.OnClickCard += AddRevealedCard;

        _cardDataArray = GameObject.Find("CardsDataBase").GetComponents<CardData>(); 
	}

    private void OpenThePack(Pack.Expansion expansion)
    {
        _currentExpansion = expansion;
        _cardsBackground.SetActive(true);
        GameObject card;
        OnCardClick onCardClick;
        for (int i = 0; i < 5; i++)
        {
            DetermineDrop();
            if(i == 4 && _currentRarity == Card.Rarity.Common)
            {
                CommonCheck();
            }

            card = ObjectPool.Instance.GetObjectForType("CardPrefab", true);
            card.transform.SetParent(_canvas.transform, false);
            card.transform.position = _packReceiver.position;
            card.transform.localScale = Vector3.one;

            onCardClick = card.GetComponent<OnCardClick>();
            onCardClick._cardRarity = _currentRarity;
            onCardClick._packExpansion = _currentExpansion;
            onCardClick._endPosition = _cardSpawnPoints[i].transform.position;
            onCardClick._currentSprite = _currentSprite;
            onCardClick._turnOverSound = _currentAudioClip;

            _activeCards.Add(onCardClick);
        }

        if (OnCardsDetermined != null)
        {
            OnCardsDetermined();
        }
    }

    private void CommonCheck()
    {
        for (int i = 0; i < _activeCards.Count; i++)
        {
            if (_activeCards[i]._cardRarity == Card.Rarity.Common)
            {
                _commonCards += 1;
                if (_commonCards == 4)
                {
                    DetermineDrop();
                    if (_currentRarity == Card.Rarity.Common)
                    {
                        _currentSprite = _cardDataArray[(int)_currentExpansion]._rareCards[Random.Range(0, _cardDataArray[(int)_currentExpansion]._rareCards.Count)];
                        _currentAudioClip = _turnOverSounds[1];
                        _currentRarity = Card.Rarity.Rare;
                        break;
                    }
                    break;
                }
            }

            else if (_activeCards[i]._cardRarity == Card.Rarity.None)
            {
                //skip card
            }

            else
            {
                //DetermineDrop();
                break;
            }
        }
        _commonCards = 0;
    }

    private void DetermineDrop()
    {
        _drop = Random.Range(0, _dropChance);

        if (_drop >= 0f && _drop <= 71f)
        {
            //common
            _currentSprite = _cardDataArray[(int)_currentExpansion]._commonCards[Random.Range(0, _cardDataArray[(int)_currentExpansion]._commonCards.Count)];
            _currentAudioClip = _turnOverSounds[0];
            _currentRarity = Card.Rarity.Common;
        }

        else if (_drop > 71f && _drop <= 94.4f)
        {
            //rare
            _currentSprite = _cardDataArray[(int)_currentExpansion]._rareCards[Random.Range(0, _cardDataArray[(int)_currentExpansion]._rareCards.Count)];
            _currentAudioClip = _turnOverSounds[1];
            _currentRarity = Card.Rarity.Rare;
        }

        else if (_drop > 94.4f && _drop <= 98.9f)
        {
            //epic
            _currentSprite = _cardDataArray[(int)_currentExpansion]._epicCards[Random.Range(0, _cardDataArray[(int)_currentExpansion]._epicCards.Count)];
            _currentAudioClip = _turnOverSounds[2];
            _currentRarity = Card.Rarity.Epic;
        }

        else
        {
            //legendary
            _currentSprite = _cardDataArray[(int)_currentExpansion]._legendaryCards[Random.Range(0, _cardDataArray[(int)_currentExpansion]._legendaryCards.Count)];
            _currentAudioClip = _turnOverSounds[3];
            _currentRarity = Card.Rarity.Legendary;
        }
    }

    public void RemoveCards()
    {
        foreach(OnCardClick card in _activeCards)
        {
            card._cardRarity = Card.Rarity.None;
            ObjectPool.Instance.PoolObject(card.gameObject);
        }
        _activeCards.Clear();
    }

    private void AddRevealedCard()
    {
        _cardsRevealed += 1;
        if(_cardsRevealed == 5)
        {
            _cardsRevealed = 0;
            _doneButton.SetActive(true);
        }
    }

    private void OnDisable()
    {
        OnDragPack.OnOpenPack -= OpenThePack;
        OnCardClick.OnClickCard -= AddRevealedCard;
    }
}
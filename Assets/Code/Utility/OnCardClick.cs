using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnCardClick : MonoBehaviour
{
    public Pack.Expansion _packExpansion;
    public Card.Rarity _cardRarity;
    
    private OpenPack _openPack;

    [SerializeField]private List<AudioClip> _turnOverSounds;

    private AudioSource _audioSource;

    private float _dropChance = 100f;
    private float _drop;
    private float _lerpTime = 0.5f;

    private int _commonCards = 0;

    private Sprite _currentSprite;

    private Animator _animator;

    public delegate void ClickCardAction();
    public static event ClickCardAction OnClickCard;

    public Vector2 _endPosition;

    void Start()
    {
        StartCoroutine(LerpPosition());
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _openPack = GameObject.Find("OpenPack").GetComponent<OpenPack>();

        List<Sprite> cardBackSprites = GameObject.Find("CardBacksDataBase").GetComponent<CardBacksData>()._cardBackSprites;
        string cardBackName = PlayerPrefs.GetString("CardBackID", "Classic");
        
        for (int i = 0; i < cardBackSprites.Count; i++)
        {
            if(cardBackSprites[i].name == cardBackName)
            {
                GetComponent<Image>().sprite = cardBackSprites[i];
                break;
            }
        }
    }

    public void CardClick()
    {
        _animator.SetBool("isClicked", true);
    }

    public void SetRandomCardSprite()
    {
        _drop = Random.Range(0, _dropChance);

        if (_openPack.CardsRevealed == 4)
        {
            for (int i = 0; i < _openPack._activeCards.Count; i++)
            {
                if (_openPack._activeCards[i].tag == Tags.COMMONTAG)
                {
                    _commonCards += 1;
                    if (_commonCards == 4)
                    {
                        DetermineDrop();
                        if(gameObject.tag == Tags.COMMONTAG)
                        {
                            _currentSprite = _openPack._cardDataArray[(int)_packExpansion]._rareCards[Random.Range(0, _openPack._cardDataArray[(int)_packExpansion]._rareCards.Count)];
                            _audioSource.clip = _turnOverSounds[1];
                            gameObject.tag = Tags.RARETAG;
                        }
                        _commonCards = 0;
                    }
                }

                else if (_openPack._activeCards[i].tag == "Untagged")
                {
                    //skip card
                }

                else
                {
                    DetermineDrop();
                    _commonCards = 0;
                    break;
                }
            }
        }

        else
        {
            DetermineDrop();
        }

        if (OnClickCard != null)
        {
            OnClickCard();
        }

        _audioSource.Play();

        GetComponent<Image>().sprite = _currentSprite;
    }

    void DetermineDrop()
    {
        if (_drop >= 0f && _drop <= 71f)
        {
            //common
            _currentSprite = _openPack._cardDataArray[(int)_packExpansion]._commonCards[Random.Range(0, _openPack._cardDataArray[(int)_packExpansion]._commonCards.Count)];
            _audioSource.clip = _turnOverSounds[0];
            gameObject.tag = Tags.COMMONTAG;
            _cardRarity = Card.Rarity.Common;
        }

        else if (_drop > 71f && _drop <= 94.4f)
        {
            //rare
            _currentSprite = _openPack._cardDataArray[(int)_packExpansion]._rareCards[Random.Range(0, _openPack._cardDataArray[(int)_packExpansion]._rareCards.Count)];
            _audioSource.clip = _turnOverSounds[1];
            gameObject.tag = Tags.RARETAG;
            _cardRarity = Card.Rarity.Rare;
        }

        else if (_drop > 94.4f && _drop <= 98.9f)
        {
            //epic
            _currentSprite = _openPack._cardDataArray[(int)_packExpansion]._epicCards[Random.Range(0, _openPack._cardDataArray[(int)_packExpansion]._epicCards.Count)];
            _audioSource.clip = _turnOverSounds[2];
            gameObject.tag = Tags.EPICTAG;
            _cardRarity = Card.Rarity.Epic;
        }

        else if (_drop > 98.9f)
        {
            //legendary
            _currentSprite = _openPack._cardDataArray[(int)_packExpansion]._legendaryCards[Random.Range(0, _openPack._cardDataArray[(int)_packExpansion]._legendaryCards.Count)];
            _audioSource.clip = _turnOverSounds[3];
            gameObject.tag = Tags.LEGENDARYTAG;
            _cardRarity = Card.Rarity.Legendary;
        }
    }

    private IEnumerator LerpPosition()
    {
        float elapsedTime = 0;

        while (elapsedTime < _lerpTime)
        {
            transform.position = Vector2.Lerp(transform.position, _endPosition, (elapsedTime/_lerpTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _endPosition;
    }
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnCardClick : MonoBehaviour {

    public Card.Rarity cardRarity;

    private CardData _cardData;
    
    private OpenPack _openPack;

    [SerializeField]private List<AudioClip> _turnOverSounds;

    private AudioSource _audioSource;

    private float _dropChance = 100f;
    private float _drop;

    private int _commonCards = 0;

    private Sprite _currentSprite;

    private Animator _animator;

    public delegate void ClickCardAction();
    public static event ClickCardAction OnClickCard;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _openPack = GameObject.Find("Pack").GetComponent<OpenPack>();
        _cardData = GameObject.Find("CardData").GetComponent<CardData>();
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
            for (int i = 0; i < 4; i++)
            {
                if (_openPack._activeCards[i].tag == Tags.COMMONTAG)
                {
                    _commonCards += 1;
                    if (_commonCards == 4)
                    {
                        _currentSprite = _cardData._rareCards[Random.Range(0, _cardData._rareCards.Count - 1)];
                        _audioSource.clip = _turnOverSounds[1];
                        gameObject.tag = Tags.COMMONTAG;
                        _commonCards = 0;
                    }
                    else
                    {
                        DetermineDrop();
                    }
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

        if (_drop >= 0 && _drop <= 71f)
        {
            //common
            _currentSprite = _cardData._commonCards[Random.Range(0, _cardData._commonCards.Count -1)];
            _audioSource.clip = _turnOverSounds[0];
            gameObject.tag = Tags.COMMONTAG;
            cardRarity = Card.Rarity.Common;
        }

        else if (_drop > 71f && _drop <= 94.4)
        {
            //rare
            _currentSprite = _cardData._rareCards[Random.Range(0, _cardData._rareCards.Count -1)];
            _audioSource.clip = _turnOverSounds[1];
            gameObject.tag = Tags.RARETAG;
            cardRarity = Card.Rarity.Rare;
        }

        else if (_drop > 94.4 && _drop <= 98.9f)
        {
            //epic
            _currentSprite = _cardData._epicCards[Random.Range(0, _cardData._epicCards.Count -1)];
            _audioSource.clip = _turnOverSounds[2];
            gameObject.tag = Tags.EPICTAG;
            cardRarity = Card.Rarity.Epic;
        }

        else if (_drop > 98.9f)
        {
            //legendary
            _currentSprite = _cardData._legendaryCards[Random.Range(0, _cardData._legendaryCards.Count -1)];
            _audioSource.clip = _turnOverSounds[3];
            gameObject.tag = Tags.LEGENDARYTAG;
            cardRarity = Card.Rarity.Legendary;
        }
    }
}
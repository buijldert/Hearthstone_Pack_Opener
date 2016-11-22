using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnCardClick : MonoBehaviour {

    private CardData _cardData;
    
    private OpenPack _openPack;

    [SerializeField]private List<AudioClip> _turnOverSounds;

    private AudioSource _audioSource;

    private float _dropChance = 100f;
    private float _drop;
    private float _turnSpeed = 200;

    private int _commonCards = 0;

    private bool _isCardClicked;
    private bool _isRotating;

    private Sprite _currentSprite;

    public delegate void ClickCardAction();
    public static event ClickCardAction OnClickCard;

	public void CardClick()
    {
        if (!_isCardClicked)
        {
            _isCardClicked = true;
            _isRotating = true;
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _openPack = GameObject.Find("Pack").GetComponent<OpenPack>();
        _cardData = GameObject.Find("CardData").GetComponent<CardData>();
    }

    void Update()
    {
        if(_isRotating)
        {
            transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime);
            if(this.transform.rotation.eulerAngles.y >= 90)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                _turnSpeed = -_turnSpeed;
                _drop = Random.Range(0, _dropChance);

                if(_openPack.CardsRevealed == 4)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        if(_openPack.ActiveCards[i].tag == Tags.COMMONTAG)
                        {
                            _commonCards += 1;
                            if(_commonCards == 4)
                            {
                                _currentSprite = _cardData._rareCards[Random.Range(0, _cardData._rareCards.Count)];
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
            else if (this.transform.rotation.eulerAngles.y <= 5)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                _isRotating = false;
            }
        }
    }

    void DetermineDrop()
    {

        if (_drop >= 0 && _drop <= 71f)
        {
            //common
            _currentSprite = _cardData._commonCards[Random.Range(0, _cardData._commonCards.Count)];
            _audioSource.clip = _turnOverSounds[0];
            gameObject.tag = Tags.COMMONTAG;
        }

        else if (_drop > 71f && _drop <= 94.4)
        {
            //rare
            _currentSprite = _cardData._rareCards[Random.Range(0, _cardData._rareCards.Count)];
            _audioSource.clip = _turnOverSounds[1];
            gameObject.tag = Tags.RARETAG;
        }

        else if (_drop > 94.4 && _drop <= 98.9f)
        {
            //epic
            _currentSprite = _cardData._epicCards[Random.Range(0, _cardData._epicCards.Count)];
            _audioSource.clip = _turnOverSounds[2];
            gameObject.tag = Tags.EPICTAG;
        }

        else if (_drop > 98.9f)
        {
            //legendary
            _currentSprite = _cardData._legendaryCards[Random.Range(0, _cardData._legendaryCards.Count)];
            _audioSource.clip = _turnOverSounds[3];
            gameObject.tag = Tags.LEGENDARYTAG;
        }
    }
}

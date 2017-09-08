using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnCardClick : MonoBehaviour
{
    public Pack.Expansion _packExpansion;
    public Card.Rarity _cardRarity;
    
    private OpenPack _openPack;

    [SerializeField]
    public AudioClip _turnOverSound;

    private AudioSource _audioSource;

    private float _dropChance = 100f;
    private float _drop;
    private float _lerpTime = 0.5f;

    private int _commonCards = 0;

    public Sprite _currentSprite;

    private Animator _animator;

    public delegate void ClickCardAction();
    public static event ClickCardAction OnClickCard;

    private Vector2 _beginPosition;
    public Vector2 _endPosition;

    [SerializeField]
    private Image _cardImage;

#region LONGPRESS
    public float durationThreshold = 0.25f;

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;
#endregion

    private void OnEnable()
    {
        StartCoroutine(LerpPosition());
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "openpacks")
        {
            _openPack = GameObject.Find("OpenPack").GetComponent<OpenPack>();
        }

        _cardImage.sprite = GameObject.Find("CardBacksDataBase").GetComponent<CardBacksData>()._cardBackSprites[PlayerPrefs.GetInt("CardBackID", 4)];
    }

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                CardHold();
            }
        }
    }

    public void PointerDown()
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }

    public void PointerUp()
    {
        isPointerDown = false;
        
        if (longPressTriggered == false)
        {
            CardClick();
        }
        else
        {
            GameObject.Find("CardGlow").GetComponent<Image>().enabled = false;
        }
    }

    public void CardClick()
    {
        _animator.SetBool("isClicked", true);
    }

    public void CardHold()
    {
        longPressTriggered = true;
        if(_animator.GetBool("isClicked") == false)
        {
            GameObject.Find("CardGlow").GetComponent<CardGlow>().ChangeGlow(_cardRarity, transform.position);
        }
    }

    public void SetCardSprite()
    {
        if (OnClickCard != null)
        {
            OnClickCard();
        }
        _audioSource.clip = _turnOverSound;
        _audioSource.Play();

        _cardImage.sprite = _currentSprite;
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

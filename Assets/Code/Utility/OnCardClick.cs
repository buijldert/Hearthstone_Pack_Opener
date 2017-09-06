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

    public Vector2 _endPosition;

    void OnEnable()
    {
        StartCoroutine(LerpPosition());
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "openpacks")
        {
            _openPack = GameObject.Find("OpenPack").GetComponent<OpenPack>();
        }
        

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

    public void SetCardSprite()
    {
        if (OnClickCard != null)
        {
            OnClickCard();
        }
        _audioSource.clip = _turnOverSound;
        _audioSource.Play();

        GetComponent<Image>().sprite = _currentSprite;
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

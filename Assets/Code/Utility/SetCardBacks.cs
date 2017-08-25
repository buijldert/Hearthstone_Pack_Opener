using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCardBacks : MonoBehaviour
{
    [SerializeField]
    private List<Image> _cardBackImages;

    private void Start()
    {
        List<Sprite> cardBackSprites = GameObject.Find("CardBacksDataBase").GetComponent<CardBacksData>()._cardBackSprites;
        for (int i = 0; i < _cardBackImages.Count; i++)
        {
            _cardBackImages[i].sprite = cardBackSprites[i];
        }
    }
}

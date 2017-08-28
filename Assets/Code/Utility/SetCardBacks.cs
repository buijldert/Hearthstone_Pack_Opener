using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Displays all available cardbacks.
public class SetCardBacks : MonoBehaviour
{
    //A list of all cardback images.
    [SerializeField]
    private List<Image> _cardBackImages;

    /// <summary>
    /// Assigns sprites of all cardbacks to images.
    /// </summary>
    private void Start()
    {
        List<Sprite> cardBackSprites = GameObject.Find("CardBacksDataBase").GetComponent<CardBacksData>()._cardBackSprites;
        for (int i = 0; i < _cardBackImages.Count; i++)
        {
            _cardBackImages[i].sprite = cardBackSprites[i];
        }
    }
}

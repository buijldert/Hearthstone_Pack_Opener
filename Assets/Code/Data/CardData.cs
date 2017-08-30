using UnityEngine;
using System.Collections.Generic;

//The sprites of all cards per expansion.
public class CardData : MonoBehaviour
{
    //The expansion id (enum).
    public Pack.Expansion _packExpansion;

    //The sprites of the common cards.
    public List<Sprite> _commonCards;
    public List<Sprite> _rareCards;
    public List<Sprite> _epicCards;
    public List<Sprite> _legendaryCards;
}

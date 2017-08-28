using UnityEngine;

//This is a dataclass that holds the buying data for the player.
public class BuyData : MonoBehaviour
{
    //The number of packs to be bought.
    public static int numberOfPacks;
    //The expansion that the packs will be bought int (default = Classic expansion).
    public static Pack.Expansion packExpansion = Pack.Expansion.Classic;
    //The gold cost of the packs.
    public static int cost;
}

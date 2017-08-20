using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePackExpansion : MonoBehaviour
{
    [SerializeField]
    private Pack.Expansion expansion;

    public void ChangeExpansion()
    {
        BuyData.packExpansion = expansion;
    }
}

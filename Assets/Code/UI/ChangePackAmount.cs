using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePackAmount : MonoBehaviour
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private int cost;

	public void ChangeAmount()
    {
        BuyData.numberOfPacks = amount;
        BuyData.cost = cost;
    }
}

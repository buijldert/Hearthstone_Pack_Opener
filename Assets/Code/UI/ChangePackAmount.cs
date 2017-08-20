using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePackAmount : MonoBehaviour {

	public void ChangeAmount(int amount)
    {
        BuyData.numberOfPacks = amount;
    }
}

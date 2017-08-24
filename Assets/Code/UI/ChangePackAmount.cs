using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChangePackAmount : MonoBehaviour
{
    [FormerlySerializedAs("amount")]
    [SerializeField]
    private int _amount;
    [FormerlySerializedAs("cost")]
    [SerializeField]
    private int _cost;
    [SerializeField]
    private Text _buyButtonText;

	public void ChangeAmount()
    {
        BuyData.numberOfPacks = _amount;
        BuyData.cost = _cost;
        _buyButtonText.text = _cost.ToString();
    }
}

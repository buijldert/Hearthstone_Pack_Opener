using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCheat : MonoBehaviour
{
    [SerializeField]
    private int _amountToCheat;

	public void CheatGold ()
    {
        GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<GoldAmount>().ChangeGold(_amountToCheat);
	}
}

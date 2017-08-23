using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldText : MonoBehaviour {

    private Text _goldText;
    private GoldAmount _goldAmount;

	// Use this for initialization
	void Start () 
    {
        _goldText = GetComponent<Text>();
        _goldAmount = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<GoldAmount>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateGoldText();
	}

    void UpdateGoldText()
    {
        _goldText.text = _goldAmount._gold.ToString();
    }
}

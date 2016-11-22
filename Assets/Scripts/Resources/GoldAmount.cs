using UnityEngine;
using System.Collections;

public class GoldAmount : MonoBehaviour {

    private int _maxGold = 99999999;
    private int _gold = 500;
    public int Gold
    {
        get 
        {
            return _gold;
        }

        set
        {
            _gold = value;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        GoldControl();
	}

    void GoldControl()
    {
        if(_gold > _maxGold)
        {
            _gold = _maxGold;
        }
    }
}

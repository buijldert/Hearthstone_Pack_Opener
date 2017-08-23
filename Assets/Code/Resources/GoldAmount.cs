using UnityEngine;

public class GoldAmount : MonoBehaviour {

    private int _maxGold = 99999999;
    public int _gold = 0;
    private Gold _goldData;

    private void Awake()
    {
        _goldData = Serializer.Load<Gold>("golddata.sav");
        if(_goldData == null)
        {
            _gold = 500;

            _goldData = new Gold
            {
                goldAmount = _gold
            };

            Serializer.Save("golddata.sav", _goldData);
        }
        else
        {
            _gold = _goldData.goldAmount;
        }

    }

    public void ChangeGold(int goldMutation)
    {
        _gold += goldMutation;

        if(_gold > _maxGold)
        {
            _gold = _maxGold;
        }
        else if(_gold < 0)
        {
            _gold = 0;
        }

        _goldData.goldAmount = _gold;
        Serializer.Save("golddata.sav", _goldData);
    }
}

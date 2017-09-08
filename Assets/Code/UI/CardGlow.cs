using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGlow : MonoBehaviour
{
    [SerializeField]
    private GameObject _colorPresetsGameObject;
    
    private ColorLerp _colorLerp;

    private ColorLerpPreset[] _colorLerpPresets;

    private void Start()
    {
        _colorLerpPresets = _colorPresetsGameObject.GetComponents<ColorLerpPreset>();
        _colorLerp = GetComponent<ColorLerp>();
    }

    public void ChangeGlow(Card.Rarity rarity, Vector3 colorLerpPos)
    {
        for (int i = 0; i < _colorLerpPresets.Length; i++)
        {
            if(rarity == _colorLerpPresets[i]._rarity)
            {
                _colorLerp.startColor = _colorLerpPresets[i]._startColor;
                _colorLerp.endColor = _colorLerpPresets[i]._endColor;
                transform.position = new Vector3(colorLerpPos.x, colorLerpPos.y - 0.1f, colorLerpPos.z);
                GetComponent<Image>().enabled = true;
                break;
            }
        }
    }
}

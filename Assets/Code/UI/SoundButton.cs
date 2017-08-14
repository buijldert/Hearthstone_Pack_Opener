using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _mutedSoundImage;
    [SerializeField]
    private Sprite _soundImage;
    [SerializeField]
    private Image _curentSoundImage;

    public void SoundToggle()
    {
        if (_curentSoundImage.sprite == _soundImage)
        {
            _curentSoundImage.sprite = _mutedSoundImage;
            AudioListener.pause = true;
        }
        else
        {
            _curentSoundImage.sprite = _soundImage;
            AudioListener.pause = false;
        }
    }
}
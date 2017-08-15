using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _mutedSoundImage;
    [SerializeField]
    private Sprite _soundImage;
    [FormerlySerializedAs("_curentSoundImage")]
    [SerializeField]
    private Image _currentSoundImage;

    public void SoundToggle()
    {
        if (_currentSoundImage.sprite == _soundImage)
        {
            _currentSoundImage.sprite = _mutedSoundImage;
            AudioListener.pause = true;
        }
        else
        {
            _currentSoundImage.sprite = _soundImage;
            AudioListener.pause = false;
        }
    }
}
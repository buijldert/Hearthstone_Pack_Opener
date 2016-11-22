using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundButton : MonoBehaviour
{
    [SerializeField]private Sprite _mutedSoundImage;
    [SerializeField]private Sprite _soundImage;
    [SerializeField]private Image _curentSoundImage;


    private SoundMuter _soundMuter;
	// Use this for initialization
	void Start()
	{
        _soundMuter = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<SoundMuter>();
        if(_soundMuter.IsSoundActive)
        {
            _curentSoundImage.sprite = _soundImage;
        }
        else
        {
            _curentSoundImage.sprite = _mutedSoundImage;
        }
	}

    void Update()
    {
        CheckWhichImage();
    }

    void CheckWhichImage()
    {
        if (_soundMuter.IsSoundActive)
        {
            _curentSoundImage.sprite = _soundImage;
        }
        else
        {
            _curentSoundImage.sprite = _mutedSoundImage;
        }
    }

    public void SoundToggle()
    {
        if (_soundMuter.IsSoundActive)
        {
            _soundMuter.IsSoundActive = false;
        }
        else
        {
            _soundMuter.IsSoundActive = true;
        }
    }
}
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundMuter : MonoBehaviour
{
    private bool _isSoundActive = true;
    public bool IsSoundActive
    {
        get
        {
            return _isSoundActive;
        }
        set
        {
            _isSoundActive = value;
        }
    }

    void Update()
    {
        CheckSound();
    }

    void CheckSound()
    {
        if(_isSoundActive)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }
}
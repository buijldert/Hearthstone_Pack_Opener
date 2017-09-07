using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{

    [SerializeField]
    private Image _imageToLerp;

    [SerializeField]
    private bool _startsLerping;

    private Color lerpedColor = Color.white;

    [SerializeField]
    public Color32 startColor = new Color32(66, 134, 244, 75);
    [SerializeField]
    public Color32 endColor = new Color32(10, 57, 132, 75);

    private Coroutine _lerpColorCoroutine;

    private void Start()
    {
        if(_startsLerping)
        {
            StartCoroutine(LerpColor());
        }
    }

    private IEnumerator LerpColor()
    {
        lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
        _imageToLerp.color = lerpedColor;
        yield return null;
        _lerpColorCoroutine = StartCoroutine(LerpColor());
    }

    public void ControlLerp(bool isLerping)
    {
        _imageToLerp.enabled = isLerping;
        if (isLerping)
        {
            _lerpColorCoroutine = StartCoroutine(LerpColor());
        }
        else
        {
            StopCoroutine(_lerpColorCoroutine);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePage : MonoBehaviour {

    private Coroutine _lerpRotation;

    private float _beginYRotation = 0;
    private float _flippedYRotation = 90;
    private float _updateTime = .5f;

    public void FlipPage()
    {
        _lerpRotation = StartCoroutine(LerpRotation(transform.eulerAngles.y, 90f));
    }

    public void FlipPageBack()
    {
        _lerpRotation = StartCoroutine(LerpRotation(transform.eulerAngles.y, 0));
    }

    private IEnumerator LerpRotation(float begin, float end)
    {
        float elapsedTime = 0;
        float yRotation = begin;

        while (elapsedTime < _updateTime)
        {
            yRotation = Mathf.Lerp(begin, end, (elapsedTime / _updateTime));
            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0f, end, 0f);
    }
}

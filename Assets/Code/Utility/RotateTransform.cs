using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    //[SerializeField]
    private float _rotateTime = 0.25f;
    [SerializeField]
    private float _beginRotation;
    [SerializeField]
    private float _endRotation;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LerpRotation());
    }

    private IEnumerator LerpRotation()
    {
        float elapsedTime = 0;
        float yRotation = _beginRotation;

        while (elapsedTime < _rotateTime)
        {
            yRotation = Mathf.Lerp(_beginRotation, _endRotation, (elapsedTime / _rotateTime));
            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0f, _endRotation, 0f);
    }
}

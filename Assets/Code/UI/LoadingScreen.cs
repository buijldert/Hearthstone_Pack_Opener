using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private float _loadingTime = .2f;
    [SerializeField]
    private Transform _outerCircle;

    [FormerlySerializedAs("loadingImage")]
    [SerializeField]
    private Sprite _loadingImage;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadTheLevel(sceneName, 0f, 90f));
    }

    private IEnumerator LoadTheLevel(string sceneName, float begin, float end)
    {
        float elapsedTime = 0f;
        float rotation = begin;
        _outerCircle.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
        while (elapsedTime < _loadingTime)
        {
            rotation = Mathf.Lerp(begin, end, (elapsedTime / _loadingTime));
            transform.eulerAngles = new Vector3(0f, rotation, 0f);
            _outerCircle.eulerAngles = new Vector3(0f, rotation, 0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0f, end, 0f);
        if(end == 90f)
        {
            StartCoroutine(LoadTheLevel(sceneName, 90f, 0f));
            GetComponent<Image>().sprite = _loadingImage;
        }
        else
        {
            elapsedTime = 0f;
            yield return new WaitForSeconds(0.1f);
            while(elapsedTime < Random.Range(0.5f, 0.75f))
            {
                _outerCircle.transform.Rotate(0, 0, -0.75f);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}

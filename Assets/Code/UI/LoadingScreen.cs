using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private float _loadingTime = .2f;

    [SerializeField]
    private Sprite loadingImage;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadTheLevel(sceneName, 0f, 90f));
    }

    private IEnumerator LoadTheLevel(string sceneName, float begin, float end)
    {
        float elapsedTime = 0f;
        float yRotation = begin;

        while (elapsedTime < _loadingTime)
        {
            yRotation = Mathf.Lerp(begin, end, (elapsedTime / _loadingTime));
            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0f, end, 0f);
        if(end == 90f)
        {
            StartCoroutine(LoadTheLevel(sceneName, 90f, 0f));
            GetComponent<Image>().sprite = loadingImage;
        }
        else
        {
            yield return new WaitForSeconds(.25f);
            SceneManager.LoadScene(sceneName);
        }
    }
}

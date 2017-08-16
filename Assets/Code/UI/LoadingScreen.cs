using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private float _loadingTime = .5f;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadTheLevel(sceneName));
    }

    private IEnumerator LoadTheLevel(string sceneName)
    {
        float elapsedTime = 0f;
        float yRotation = 90f;

        while (elapsedTime < _loadingTime)
        {
            yRotation = Mathf.Lerp(90f, 0f, (elapsedTime / _loadingTime));
            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = new Vector3(0f, 90f, 0f);
        SceneManager.LoadScene(sceneName);
    }
}

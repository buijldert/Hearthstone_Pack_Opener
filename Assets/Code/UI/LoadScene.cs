using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private float loadDelay = 0f;
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneDelay(sceneName));
    }

    private IEnumerator ChangeSceneDelay(string sceneName)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
}

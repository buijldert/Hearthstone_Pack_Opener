using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    [SerializeField]private float _delayTime;
    [SerializeField]private string _sceneName;

    public void ChangeScene()
    {
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(_delayTime);
        SceneManager.LoadScene(_sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackButton : MonoBehaviour
{
    void Update()
    {
        UseBackButton();
    }

    void UseBackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != SceneNames.MAINMENUSCENE)
            {
                SceneManager.LoadScene(SceneNames.MAINMENUSCENE);
            }
            else if (SceneManager.GetActiveScene().name == SceneNames.MAINMENUSCENE)
            {
                Application.Quit();
            }
        }
    }
    //private string _previousScene;
    //private string _currentScene;
	// Use this for initialization
    /*void Awake()
    {
        if (_currentScene == null)
        {
            _currentScene = SceneManager.GetActiveScene().name;
        }
    }*

	void OnLevelWasLoaded()
	{
        _currentScene = SceneManager.GetActiveScene().name;
	}*/
	
	// Update() is called once per frame
	

    /*public void OnExitScene()
    {
        _previousScene = _currentScene;
    }*/
}
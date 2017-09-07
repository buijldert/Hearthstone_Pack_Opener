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
}
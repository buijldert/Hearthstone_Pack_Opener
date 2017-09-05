using UnityEngine;
using System.Collections;

public class DoneButton : MonoBehaviour {

    public delegate void ClosePackAction();
    public static event ClosePackAction OnClosePack;

    [SerializeField]private GameObject _cardsBackground;

	public void Done()
    {
        
        StartCoroutine(ClosePackDelay());
    }

    private IEnumerator ClosePackDelay()
    {
        yield return new WaitForEndOfFrame();
        if (OnClosePack != null)
        {
            OnClosePack();
        }
        gameObject.SetActive(false);
        _cardsBackground.SetActive(false);
        
    }
}

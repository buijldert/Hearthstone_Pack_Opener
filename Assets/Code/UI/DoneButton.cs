using UnityEngine;
using System.Collections;

public class DoneButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardsBackground;

	public void Done()
    {
        StartCoroutine(ClosePackDelay());
    }

    private IEnumerator ClosePackDelay()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
        _cardsBackground.SetActive(false);
    }
}

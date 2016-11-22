using UnityEngine;
using System.Collections;

public class DoneButton : MonoBehaviour {

    public delegate void ClosePackAction();
    public static event ClosePackAction OnClosePack;

    [SerializeField]private GameObject _cardsBackground;

    void Start()
    {
        _cardsBackground.SetActive(false);
        gameObject.SetActive(false);
    }

	public void Done()
    {
        if(OnClosePack != null)
        {
            OnClosePack();
        }
        _cardsBackground.SetActive(false);
        gameObject.SetActive(false);
    }
}

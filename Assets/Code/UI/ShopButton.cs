using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public delegate void ShopButtonClickedAction();
    public static event ShopButtonClickedAction OnShopButtonClicked;

    [SerializeField]
    private GameObject _currentPackPile;

    private Outline _outline;

    private void OnEnable()
    {
        OnShopButtonClicked += DisablePacksPile;
        _outline = GetComponent<Outline>();
    }

	public void ChangePackAmount()
    {
        if(OnShopButtonClicked != null)
        {
            OnShopButtonClicked();
        }
        _currentPackPile.SetActive(true);
        _outline.enabled = true;
    }

    private void DisablePacksPile()
    {
        _currentPackPile.SetActive(false);
        _outline.enabled = false;
    }

    private void OnDisable()
    {
        OnShopButtonClicked -= DisablePacksPile;
    }
}

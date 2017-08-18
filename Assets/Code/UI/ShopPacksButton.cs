using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPacksButton : MonoBehaviour
{
    public delegate void ShopPacksButtonAction();
    public static event ShopPacksButtonAction OnShopPacksButton;

    private Outline _outline;

    [SerializeField]
    private List<Image> _packs;

    private void OnEnable()
    {
        OnShopPacksButton += DisablePackOutline;
        _outline = GetComponent<Outline>();
    }

    public void ChangePacks()
    {
        if (OnShopPacksButton != null)
        {
            OnShopPacksButton();
        }
        _outline.enabled = true;
        for (int i = 0; i < _packs.Count; i++)
        {
            _packs[i].sprite = GetComponent<Image>().sprite;
        }
    }

    private void DisablePackOutline()
    {
        _outline.enabled = false;
    }

    private void OnDisable()
    {
        OnShopPacksButton -= DisablePackOutline;
    }
}

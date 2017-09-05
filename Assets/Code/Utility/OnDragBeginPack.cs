using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnDragBeginPack : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject _packPrefab;
    [SerializeField]
    private GameObject _counterGameObject;

    [SerializeField]
    private Transform _canvasParent;

    [SerializeField]
    private Transform _normalParent;

    public Pack.Expansion _packExpansion;

    private Image _packImage;
    private Text _counterText;

    public int _packCount;

    private bool _isPackEnabled = true;

    private void Start()
    {
        _counterText = GetComponentInChildren<Text>();
        _counterGameObject = _counterText.transform.parent.gameObject;
        _packImage = GetComponent<Image>();
        ChangePackText();
        if(_packCount <= 0)
        {
            ChangePackVisuals(false);
            _isPackEnabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_isPackEnabled)
        {
            GameObject pack = ObjectPool.Instance.GetObjectForType("Pack", true);
            pack.transform.localScale = Vector3.one;
            pack.transform.SetParent(_canvasParent, false);
            pack.GetComponent<Image>().sprite = _packImage.sprite;
            pack.GetComponent<OnDragPack>()._onDragBeginPack = this;
            _packCount -= 1;
            ChangePackText();
            if (_packCount <= 0)
            {
                ChangePackVisuals(false);
                _isPackEnabled = false;
            }
        }
    }

    public void AddPack()
    {
        _packCount += 1;
        ChangePackVisuals(true);
        _isPackEnabled = true;
    }

    private void ChangePackVisuals(bool isEnabled)
    {
        //Color32 imageColor = _packImage.color;
        //imageColor.a = alpha;
        //_packImage.color = imageColor;
        if(isEnabled)
        {
            transform.SetParent(_normalParent);
        }
        else
        {
            transform.SetParent(null);
        }

        ChangePackText();
    }

    private void ChangePackText()
    {
        _counterText.text = _packCount.ToString();
        if (_packCount < 2)
        {
            _counterGameObject.SetActive(false);
        }
        else
        {
            _counterGameObject.SetActive(true);
        }
    }
}

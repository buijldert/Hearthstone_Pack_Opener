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
    private Transform _canvasParent;

    public Pack.Expansion _packExpansion;

    private Image _packImage;

    public int _packCount;

    private bool _isPackEnabled = true;

    private void Start()
    {
        _packImage = GetComponent<Image>();
        if(_packCount <= 0)
        {
            Color32 imageColor = _packImage.color;
            imageColor.a = 30;
            _packImage.color = imageColor;
            _isPackEnabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_isPackEnabled)
        {
            GameObject pack = Instantiate(_packPrefab, transform.position, transform.rotation, _canvasParent);
            pack.GetComponent<Image>().sprite = _packImage.sprite;
            pack.GetComponent<OnDragPack>()._onDragBeginPack = this;
            _packCount -= 1;
            if (_packCount <= 0)
            {
                Color32 imageColor = _packImage.color;
                imageColor.a = 30;
                _packImage.color = imageColor;
                _isPackEnabled = false;
            }
        }
    }

    public void AddPack()
    {
        _packCount += 1;
        Color32 imageColor = _packImage.color;
        imageColor.a = 255;
        _packImage.color = imageColor;
        _isPackEnabled = true;
    }
}

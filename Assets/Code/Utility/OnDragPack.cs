using UnityEngine;
using System.Collections;

public class OnDragPack : MonoBehaviour
{    
    private Transform _packReceiver;
    private Vector2 _beginPos;
    private float _distance;
    private float _maxDistance = 125;

    public delegate void OpenPackAction();
    public static event OpenPackAction OnOpenPack;

    void Start()
    {
        _beginPos = transform.position;
        OpenPack.OnReturnPack += ReturnToBeginPosition;
        _packReceiver = GameObject.Find("PackReceiver").transform;
    }

    public void OnDrag() 
    { 
        transform.position = Input.mousePosition;
    }

    public void OnStopDrag()
    {
        _distance = Vector3.Distance(transform.position, _packReceiver.position);
        if(_distance < _maxDistance)
        {
            transform.position = _packReceiver.transform.position;
            if(OnOpenPack != null)
            {
                OnOpenPack();
            }
        }
        else
        {
            ReturnToBeginPosition();
        }
    }

    void ReturnToBeginPosition()
    {
        transform.position = _beginPos;
    }

    void OnDisable()
    {
        OpenPack.OnReturnPack -= ReturnToBeginPosition;
    }
}

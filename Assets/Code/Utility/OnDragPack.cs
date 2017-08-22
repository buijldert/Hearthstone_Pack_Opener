using UnityEngine;
using System.Collections;

public class OnDragPack : MonoBehaviour
{
    public OnDragBeginPack _onDragBeginPack;
    private Transform _packReceiver;
    private Vector2 _beginPos;
    private float _distance;
    private float _maxDistance = 50f;

    public delegate void OpenPackAction();
    public static event OpenPackAction OnOpenPack;

    void Start()
    {
        _beginPos = transform.position;
        OpenPack.OnReturnPack += ReturnToBeginPosition;
        _packReceiver = GameObject.Find("PackReceiver").transform;
    }

    private void Update() 
    { 
        if(Input.GetMouseButtonUp(0))
        {
            OnStopDrag();
        }
        else
        {
            transform.position = Input.mousePosition;
        }
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
                Destroy(gameObject);
            }
        }
        else
        {
            _onDragBeginPack.AddPack();
            Destroy(gameObject);
            ///ReturnToBeginPosition();
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

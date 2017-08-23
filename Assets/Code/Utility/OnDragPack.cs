using UnityEngine;
using System.Collections;

public class OnDragPack : MonoBehaviour
{
    public OnDragBeginPack _onDragBeginPack;

    public Pack.Expansion _packExpansion;

    private Transform _packReceiver;
    private float _distance;
    private float _maxDistance = 50f;

    public delegate void OpenPackAction(Pack.Expansion packExpansion);
    public static event OpenPackAction OnOpenPack;

    void Start()
    {
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
                OnOpenPack(_onDragBeginPack._packExpansion);
                
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
}

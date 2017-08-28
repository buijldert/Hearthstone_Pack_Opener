using UnityEngine;
using System.Collections;

public class OnDragPack : MonoBehaviour
{
    public OnDragBeginPack _onDragBeginPack;

    public Pack.Expansion _packExpansion;

    private Transform _packReceiver;
    private float _distance;
    private float _maxDistance = 5f;

    public delegate void OpenPackAction(Pack.Expansion packExpansion);
    public static event OpenPackAction OnOpenPack;

    private bool _isOpeningPack = false;

    [SerializeField]
    private GameObject _packOpenExplosion;

    void Start()
    {
        _packReceiver = GameObject.Find("PackReceiver").transform;
    }

    private void Update() 
    {
        if (_isOpeningPack == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnStopDrag();
            }
            else
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            }
        }
    }

    public void OnStopDrag()
    {
        if(_isOpeningPack == false)
        {
            _distance = Vector2.Distance(transform.position, _packReceiver.position);
            if (_distance < _maxDistance)
            {
                transform.position = _packReceiver.transform.position;
                GetComponent<Animator>().enabled = true;
                _isOpeningPack = true;
                StartCoroutine(OpenPackDelay());
            }
            else
            {
                _onDragBeginPack.AddPack();
                Destroy(gameObject);
                ///ReturnToBeginPosition();
            }
        } 
    }

    private IEnumerator OpenPackDelay()
    {
        while (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("openPack"))
        {
            yield return new WaitForEndOfFrame();
        }
        if (OnOpenPack != null)
        {
            OnOpenPack(_packExpansion);
            Instantiate(_packOpenExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

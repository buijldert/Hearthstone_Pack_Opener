using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnDragPack : MonoBehaviour
{
    public OnDragBeginPack _onDragBeginPack;

    public Pack.Expansion _packExpansion;

    private Transform _packReceiver;
    private float _distance;
    private float _maxDistance = 150f;
    private float _startWidth;
    private float _startHeight;

    public delegate void OpenPackAction(Pack.Expansion packExpansion);
    public static event OpenPackAction OnOpenPack;

    private bool _isOpeningPack = false;

    private GameObject _eventSystem;

    private ParticleSystemRenderer[] _dragParticles;

    private ColorLerp _colorLerp;

    private Animator _animator;

    private RectTransform _rectTransform;

    private BackButton _backButton;

    private PackTrail _packTrail;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
        StartCoroutine(EnableDelay());
    }

    private IEnumerator EnableDelay()
    {
        yield return new WaitForEndOfFrame();
        //_eventSystem.enabled = false;
        if (SceneManager.GetActiveScene().name == "openpacks")
        {
            _packReceiver = GameObject.Find("PackReceiver").transform;
            _colorLerp = _packReceiver.GetComponent<ColorLerp>();
            _dragParticles = GameObject.Find("FlowContainer").GetComponentsInChildren<ParticleSystemRenderer>();
            _eventSystem = GameObject.Find("EventSystem");
            _dragParticles = GameObject.Find("FlowContainer").GetComponentsInChildren<ParticleSystemRenderer>();
            _eventSystem = GameObject.Find("EventSystem");
            _backButton = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<BackButton>();
            _backButton.enabled = false;
            _packTrail = GameObject.Find("PackTrail").GetComponent<PackTrail>();
        }
        _packExpansion = _onDragBeginPack._packExpansion;
        ChangeFlowVisibility(1);
        _colorLerp.ControlLerp(true);
        _packTrail.packToFollow = transform;
        _packTrail.GetComponent<ParticleSystem>().Play();
    }

    private void Update() 
    {
        if (_isOpeningPack == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _eventSystem.SetActive(false);
                OnStopDrag();
            }
            else
            {
                //print(Vector2.Distance(transform.position, Camera.main.WorldToScreenPoint(_packReceiver.position)));
                transform.position = /*Camera.main.ScreenToWorldPoint(*/new Vector3(Input.mousePosition.x, Input.mousePosition.y);//, 5f));
            }
        }
    }

    public void OnStopDrag()
    {
        ChangeFlowVisibility(-1);
        _colorLerp.ControlLerp(false);
        if (_isOpeningPack == false)
        {
            _distance = Vector2.Distance(transform.position, Camera.main.WorldToScreenPoint(_packReceiver.position));
            _startWidth = _rectTransform.rect.width;
            _startHeight = _rectTransform.rect.height;
            if (_distance < _maxDistance)
            {
                transform.position = Camera.main.WorldToScreenPoint(_packReceiver.position);
                
                GetComponent<Animator>().enabled = true;
                _isOpeningPack = true;
                StartCoroutine(OpenPackDelay());
            }
            else
            {
                _eventSystem.SetActive(true);
                _backButton.enabled = true;
                _onDragBeginPack.AddPack();
                RemovePack();
            }
            _packTrail.GetComponent<ParticleSystem>().Clear();
            _packTrail.GetComponent<ParticleSystem>().Pause();
        } 
    }

    private IEnumerator OpenPackDelay()
    {
        yield return new WaitForSeconds(0.5f);

        transform.SetParent(GameObject.Find("Canvas").transform, false);

        //Whirl11
        GameObject openingParticles = ObjectPool.Instance.GetObjectForType("Whirl_04", true);
        openingParticles.transform.position = new Vector3(2.30f, -0.46f, 0);

        yield return new WaitForSeconds(0.5f);

        if (OnOpenPack != null)
        {
            OnOpenPack(_packExpansion);
        }

        ObjectPool.Instance.PoolObject(openingParticles);

        //Star_Burst_02
        openingParticles = ObjectPool.Instance.GetObjectForType("Star_Burst_02", true);
        openingParticles.transform.position = new Vector3(2.46f, -0.46f, 0);

        _eventSystem.SetActive(true);

        RemovePack();
    }

    private void RemovePack()
    {
        _animator.enabled = false;
        _isOpeningPack = false;
        _rectTransform.sizeDelta = new Vector2(_startWidth, _startHeight);
        
        ObjectPool.Instance.PoolObject(gameObject);
    }

    private void ChangeFlowVisibility(int sortingOrder)
    {
        for (int i = 0; i < _dragParticles.Length; i++)
        {
            _dragParticles[i].sortingOrder = sortingOrder;
        }
    }
}

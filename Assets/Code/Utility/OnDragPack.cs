using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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

    private EventSystem _eventSystem;

    private ParticleSystemRenderer[] _dragParticles;

    private ColorLerp _colorLerp;

    void Start()
    {
        _packReceiver = GameObject.Find("PackReceiver").transform;
        _colorLerp = _packReceiver.GetComponent<ColorLerp>();
        _dragParticles = GameObject.Find("FlowContainer").GetComponentsInChildren<ParticleSystemRenderer>();
        _eventSystem = EventSystem.current;
        _eventSystem.enabled = false;
        ChangeFlowVisibility(1);
        _colorLerp.ControlLerp(true);
    }

    private void Update() 
    {
        if (_isOpeningPack == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _eventSystem.enabled = false;
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
        ChangeFlowVisibility(-1);
        _colorLerp.ControlLerp(false);
        if (_isOpeningPack == false)
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
                _eventSystem.enabled = true;
                _onDragBeginPack.AddPack();
                Destroy(gameObject);
            }
        } 
    }

    private IEnumerator OpenPackDelay()
    {
        yield return new WaitForSeconds(0.5f);

        //ParticleSystemRenderer packGlow = GameObject.Find("Cell_02").GetComponent<ParticleSystemRenderer>();
        //ParticleSystem packGlowParticleSystem = packGlow.gameObject.GetComponent<ParticleSystem>();
        
        //packGlow.sortingOrder = 0;
        //packGlowParticleSystem.Play();

        yield return new WaitForSeconds(0.5f);

        //packGlow.sortingOrder = -1;
        //packGlowParticleSystem.Pause();

        if (OnOpenPack != null)
        {
            OnOpenPack(_packExpansion);
        }

        //ForkParticleEffect explosionEffect = GameObject.Find("Stylized_Space_Explosion").GetComponent<ForkParticleEffect>();
        //RestartEffect();
        //explosionEffect.PlayEffect();

        _eventSystem.enabled = true;

        Destroy(gameObject);
    }

    private void ChangeFlowVisibility(int sortingOrder)
    {
        for (int i = 0; i < _dragParticles.Length; i++)
        {
            _dragParticles[i].sortingOrder = sortingOrder;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private ParticleSystemRenderer[] _dragParticles;

    void Start()
    {
        _packReceiver = GameObject.Find("PackReceiver").transform;
        _dragParticles = GameObject.Find("FlowContainer").GetComponentsInChildren<ParticleSystemRenderer>();
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
                ChangeFlowVisibility(0);
            }
        }
    }

    public void OnStopDrag()
    {
        if(_isOpeningPack == false)
        {
            ChangeFlowVisibility(-1);
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
            }
        } 
    }

    private IEnumerator OpenPackDelay()
    {
        yield return new WaitForSeconds(0.5f);

        ParticleSystemRenderer packGlow = GameObject.Find("Cell_02").GetComponent<ParticleSystemRenderer>();
        ParticleSystem packGlowParticleSystem = packGlow.gameObject.GetComponent<ParticleSystem>();
        
        packGlow.sortingOrder = 0;
        packGlowParticleSystem.Play();

        yield return new WaitForSeconds(0.5f);

        packGlow.sortingOrder = -1;
        packGlowParticleSystem.Pause();

        if (OnOpenPack != null)
        {
            OnOpenPack(_packExpansion);

            ForkParticleEffect explosionEffect = GameObject.Find("Stylized_Space_Explosion").GetComponent<ForkParticleEffect>();
            explosionEffect.RestartEffect();
            explosionEffect.PlayEffect();

            Destroy(gameObject);
        }
    }

    private void ChangeFlowVisibility(int sortingOrder)
    {
        for (int i = 0; i < _dragParticles.Length; i++)
        {
            _dragParticles[i].sortingOrder = sortingOrder;
        }
    }
}

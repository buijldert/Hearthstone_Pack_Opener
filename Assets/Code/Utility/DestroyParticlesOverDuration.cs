using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesOverDuration : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private float _destroyDelay;

    private float _waitingTime;

    private void OnEnable()
    {
        if (_destroyDelay > 0)
            _waitingTime = _destroyDelay;
        else
            _waitingTime = _particleSystem.main.duration;

        StartCoroutine(DestroyOverTime());
    }

    private IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(_waitingTime);
        ObjectPool.Instance.PoolObject(gameObject);
        //Destroy(gameObject);
    }
}

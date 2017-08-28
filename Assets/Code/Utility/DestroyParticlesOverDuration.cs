using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesOverDuration : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void Start()
    {
        
        StartCoroutine(DestroyOverCount());
    }

    private IEnumerator DestroyOverCount()
    {
        yield return new WaitForSeconds(_particleSystem.main.duration);
        Destroy(gameObject);
    }
}

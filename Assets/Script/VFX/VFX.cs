using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class VFX : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable() 
    {
        StartCoroutine(WaitForDestroy());
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(particleSystem.main.duration + particleSystem.startLifetime);
        VFX_Pool.VFX_PassAwayPool.Release(particleSystem);
    }
}

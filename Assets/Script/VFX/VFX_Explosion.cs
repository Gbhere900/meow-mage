using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFX_Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

   static public Action<VFX_Explosion> OnRecycled;
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
        OnRecycled.Invoke(this);
    }
}

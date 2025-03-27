using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;


   static public Action<VFX> OnRecycled;
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

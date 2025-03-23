using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyHealth : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health = 10;

    [Header("粒子效果")]
    [SerializeField] private ParticleSystem VFX_passaway;
    private DamageUI damageUI;

    ObjectPool<TextMeshPro> TMPPool  ;

    static public Action<float, Vector3> OnReceivedDamage;
    private void Awake()
    {
        TMPPool = TMP_Pool_damage.Instance;
        damageUI = GetComponent<DamageUI>();
    }
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button]
    public void ReceiveDamage(float damage = 8)
    {
        TextMeshPro damageText= TMPPool.Get();
        damageText.transform.position = transform.position;
       // OnReceivedDamage.Invoke(damage,transform.position);
        health -= Math.Min(health, damage);
        if (health == 0)
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        ParticleSystem VFX_passAway = VFX_Pool.VFX_PassAwayPool.Get();
        VFX_passAway.transform.position = transform.position;
        Destroy(gameObject);
        //VFX_passaway.transform.parent = null;
        //VFX_passaway.Play();


    }
}

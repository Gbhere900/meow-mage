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


    [Header("伤害文字效果")]
    static public Action<float, Vector3> OnReceivedDamage;
    static public Action<Vector3> OnPassAway;
    private void Awake()
    {

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
        OnReceivedDamage.Invoke(damage, transform.position);
        //DamageUI damageText = TMP_Pool.TMP_damageTextPool.Get();
        //damageText.transform.position = transform.position;
        //damageText.PlayDamageUIAnimation(damage);
        // OnReceivedDamage.Invoke(damage,transform.position);
        health -= Math.Min(health, damage);
        if (health == 0)
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        OnPassAway.Invoke(transform.position);
       // ParticleSystem VFX_passAway = VFX_Pool.VFX_PassAwayPool.Get();
       // VFX_passAway.transform.position = transform.position;
        Destroy(gameObject);
        //VFX_passaway.transform.parent = null;
        //VFX_passaway.Play();


    }
}

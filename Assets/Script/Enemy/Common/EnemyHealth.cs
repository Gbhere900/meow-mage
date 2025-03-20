using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DamageUI))]
public class EnemyHealth : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health = 10;

    [Header("粒子效果")]
    [SerializeField] private ParticleSystem VFX_passaway;
    private DamageUI damageUI;
    // Start is called before the first frame update
    private void Awake()
    {
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

    void ReceiveDamage(float damage)
    {
        health -= Math.Min(health, damage);
        damageUI.PlayDamageUIAnimation();
        if (health == 0)
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        VFX_passaway.transform.parent = null;
        VFX_passaway.Play();
        Destroy(gameObject);

    }
}

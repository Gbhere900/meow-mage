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
    [SerializeField] private int EXPnum = 3;
    [SerializeField] private int Goldnum = 3;


    [Header("粒子效果")]


    [Header("伤害文字效果")]
    [SerializeField] static public Action<float, Vector3> OnReceivedDamage;
    static public Action<Vector3> OnPassAway;
    static public Action<Vector3,int,int> OnGeneratingCollectable;
    private void Awake()
    {

    }
    private void OnEnable()
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
        health -= Math.Min(health, damage);
        if (health == 0)
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        OnPassAway?.Invoke(transform.position);
        OnGeneratingCollectable?.Invoke(transform.position,EXPnum,Goldnum);
        Destroy(gameObject);
    }
    public void PassAwayOnSwitchWave()
    {
        OnPassAway?.Invoke(transform.position);
        Destroy(gameObject);
    }
}

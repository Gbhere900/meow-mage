using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TMPPoolManager : MonoBehaviour
{
    private ObjectPool<DamageUI> damageUIPool;

    private void Awake()
    {
        //DamageUI²¿·Ö
        
        DamageUIPool.Init();
        damageUIPool = DamageUIPool.Instance;

    }

    private void OnEnable()
    {
        EnemyHealth.OnReceivedDamage += SpawnDamageUI;
        DamageUI.OnRecycled += RecycleDamageUI;
    }

    private void OnDisable()
    {
        EnemyHealth.OnReceivedDamage -= SpawnDamageUI;
        DamageUI.OnRecycled -= RecycleDamageUI;
    }
    void SpawnDamageUI(float damage,Vector3 position)
    {
        DamageUI tempDamageUI =  damageUIPool.Get();
        tempDamageUI.transform.position = position;
        tempDamageUI.Spawn(damage);
    }

    private void RecycleDamageUI(DamageUI damageUI) 
    {
        damageUIPool.Release(damageUI);
    }

    // Update is called once per frame
}

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
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        EnemyHealth.OnReceivedDamage -= SpawnDamageUI;
        DamageUI.OnRecycled -= RecycleDamageUI;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void SpawnDamageUI(float damage,Vector3 position)
    {
        DamageUI tempDamageUI =  damageUIPool.Get();
        tempDamageUI.transform.position = position+ new Vector3(Random.Range(-0.3f,0.3f),0,Random.Range(-0.3f, 0.3f));
        tempDamageUI.Spawn(damage);
    }

    private void RecycleDamageUI(DamageUI damageUI) 
    {
        damageUIPool.Release(damageUI);
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        DamageUIPool._instance = null;
    }
        // Update is called once per frame

}

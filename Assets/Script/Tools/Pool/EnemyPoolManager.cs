using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolManager : MonoBehaviour
{
    private ObjectPool<EnemyHealth> enemyPool;


    private void Awake()
    {
        EnemyPool.Init();  
        enemyPool = EnemyPool.Instance;
    }
    private void Start()
    {
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        for(int i = 0;i<100;i++)
        {
            yield return new WaitForSeconds(1f);
            EnemyHealth enemy = enemyPool.Get();
            enemy.transform.position = Vector3.zero + new Vector3(i, 0, 0);
        }
        
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}

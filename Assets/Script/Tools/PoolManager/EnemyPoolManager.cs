using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



//´ýÊµÏÖ



public class EnemyPoolManager : MonoBehaviour
{
    private ObjectPool<EnemySpawnSpot> enemyPool_slime;


    private void Awake()
    {
        EnemyPool_slime.Init();  
        enemyPool_slime = EnemyPool_slime.Instance;
    }
    private void Start()
    {
        //StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        for(int i = 0;i<100;i++)
        {
            yield return new WaitForSeconds(1f);
            EnemySpawnSpot enemy = enemyPool_slime.Get();
            enemy.transform.position = Vector3.zero + new Vector3(i, 0, 0);
        }
        
    } 


    void GenerateSlime(Vector3 position)
    {
        EnemySpawnSpot tempEnemeSpawnSpot =  enemyPool_slime.Get();
        tempEnemeSpawnSpot.transform.position = position;

    }
    
}

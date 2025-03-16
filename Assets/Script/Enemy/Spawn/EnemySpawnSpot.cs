using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSpot : MonoBehaviour
{
    //等之后可能会创建敌人的父类，再把gameobject改成敌人父类
    [SerializeField] private GameObject enemytoSpawn;
    
    public float SecondsToWait = 2f;
    void Start()
    {
        //播放缩放动画
        StartCoroutine(WaitForSpawn());
    }


    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(SecondsToWait);
        SpawnEnemy();
        GameObject.Destroy(this.gameObject);
    }

    private void SpawnEnemy()
    {
        Transform enemyTransfrom = enemytoSpawn.transform;
        enemyTransfrom.position = transform.position;
        GameObject.Instantiate(enemytoSpawn, enemyTransfrom);
    }
}

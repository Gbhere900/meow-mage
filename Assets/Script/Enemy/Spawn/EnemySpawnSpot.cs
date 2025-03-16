using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSpot : MonoBehaviour
{
    //��֮����ܻᴴ�����˵ĸ��࣬�ٰ�gameobject�ĳɵ��˸���
    [SerializeField] private GameObject enemytoSpawn;
    
    public float SecondsToWait = 2f;
    void Start()
    {
        //�������Ŷ���
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
        GameObject.Instantiate(enemytoSpawn,transform.position,enemytoSpawn.transform.rotation);
    }
}

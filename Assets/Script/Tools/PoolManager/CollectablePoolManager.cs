using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CollectablePoolManager : MonoBehaviour
{
    ObjectPool<EXPBall> EXPBallPool_instance;
    ObjectPool<GoldBall> GoldBallPool_instance;
    private void Awake()
    {
        EXPBallPool.Init();
        EXPBallPool_instance = EXPBallPool.Instance;

        GoldBallPool.Init();
        GoldBallPool_instance = GoldBallPool.Instance;
    }
    private void OnEnable()
    {
        EnemyHealth.OnGeneratingCollectable += GenerateCollectable;

        EXPBall.OnRecycled += RecycleEXPBall;
        GoldBall.OnRecycled += RecycleGoldBall;


    }
    private void OnDisable() 
    {
        EnemyHealth.OnGeneratingCollectable -= GenerateCollectable;

        EXPBall.OnRecycled -= RecycleEXPBall;
        GoldBall.OnRecycled -= RecycleGoldBall;
    }

    void GenerateCollectable(Vector3 position,int EXPNum,int goldNum)
    {
        for (int i = 0; i < EXPNum; i++)
        {
            EXPBall tempEXPBall = EXPBallPool_instance.Get();
            tempEXPBall.transform.position = position+new Vector3(Random.Range(-0.5f,0.5f),0, Random.Range(-0.5f, 0.5f));
        }
        for (int i = 0; i < goldNum; i++)
        {
            GoldBall tempGoldBall = GoldBallPool_instance.Get();
            tempGoldBall.transform.position = position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        }
    }


    void RecycleEXPBall(EXPBall tempEXPBall) => EXPBallPool_instance.Release(tempEXPBall);
   

    void RecycleGoldBall(GoldBall tempGoldBall) => GoldBallPool_instance.Release(tempGoldBall);
}
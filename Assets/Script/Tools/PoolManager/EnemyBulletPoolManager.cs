
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPoolManager : MonoBehaviour
{
    static EnemyBulletPoolManager instance;
    private ObjectPool<SlimeGelBullet> slimeGelBulletPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
            Destroy(gameObject);
        SlimeGelBulletPool.Init();
        slimeGelBulletPool = SlimeGelBulletPool.Instance;

    }
    static public EnemyBulletPoolManager Instance()
    {
        return instance;
    }

    private void OnEnable()
    {
        SlimeGelBullet.OnRecycled += RecycleSlimeGelBullet;
    }

    private void OnDisable()
    {
        SlimeGelBullet.OnRecycled -= RecycleSlimeGelBullet;
    }
    public SlimeGelBullet SpawnSlimeGelBullet (Vector3 position)
    {
        SlimeGelBullet slimeGelBullet = slimeGelBulletPool.Get();
        
        return slimeGelBullet;
    }

    private void RecycleSlimeGelBullet(SlimeGelBullet slimeGelBullet)
    {
        slimeGelBulletPool.Release(slimeGelBullet);
    }
}

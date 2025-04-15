using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    //magicBullet
    ObjectPool<Bullet> magicBulletPool;

    private void Awake()
    {
        MagicBulletPool.Init();
        magicBulletPool = MagicBulletPool.Instance;
    }
    private void OnEnable()
    {
        PlayerAttack.OnplayerAttack += SpawnMagicBullet;
        MagicBullet.OnRecycled += RecycleMagicBullet;
    }
    private void OnDisable()
    {

        PlayerAttack.OnplayerAttack -= SpawnMagicBullet;
        MagicBullet.OnRecycled += RecycleMagicBullet;
    }

    private void SpawnMagicBullet(PlayerAttack playerAttack)
    {
        Bullet tempBullet =  magicBulletPool.Get();
        tempBullet.transform.position = playerAttack.transform.position;
        tempBullet.shootByDirection();
    }

   private void  RecycleMagicBullet(Bullet bullet)
    {
        magicBulletPool.Release(bullet);
    }

}

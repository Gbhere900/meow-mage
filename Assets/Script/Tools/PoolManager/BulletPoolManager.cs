using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    //magicBullet
    ObjectPool<Bullet> magicBulletPool;
    ObjectPool<Bullet> boomPool;
    ObjectPool<Bullet> magicArrowPool;
    private void Awake()
    {
        MagicBulletPool.Init();
        BoomPool.Init();
        MagicArrowPool.Init();
        magicBulletPool = MagicBulletPool.Instance;
        boomPool = BoomPool.Instance;
        magicArrowPool = MagicArrowPool.Instance;
    }
    private void OnEnable()
    {
        PlayerAttack.OnplayerAttack += SpawnMagicBullet;
        MagicBullet.OnRecycled += RecycleMagicBullet;
        Boom.OnRecycled += RecycleBoom;
        MagicArrow.OnRecycled += RecycleMagicArrow;
    }
    private void OnDisable()
    {

        PlayerAttack.OnplayerAttack -= SpawnMagicBullet;
        MagicBullet.OnRecycled -= RecycleMagicBullet;
        Boom.OnRecycled -= RecycleBoom;
        MagicArrow.OnRecycled -= RecycleMagicArrow;
    }

    private void SpawnMagicBullet(PlayerAttack playerAttack)
    {
        Bullet tempBullet =  magicBulletPool.Get();
        //tempBullet.transform.position = playerAttack.transform.position;
        //tempBullet.shootByDirection();

        tempBullet = magicArrowPool.Get();
        tempBullet.transform.position = playerAttack.transform.position;
        tempBullet.shootByDirection();

        //tempBullet = boomPool.Get();
        //tempBullet.transform.position = playerAttack.transform.position;
        //tempBullet.shootByDirection();
    }

   private void  RecycleMagicBullet(Bullet bullet)
    {
        magicBulletPool.Release(bullet);
    }
    private void RecycleBoom(Bullet bullet)
    {
        boomPool.Release(bullet);
    }

    private void RecycleMagicArrow(Bullet bullet)
    {
        magicArrowPool.Release(bullet);
    }


}

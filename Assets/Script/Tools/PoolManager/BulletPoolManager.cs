using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    //magicBullet
    ObjectPool<MagicBullet> magicBulletPool;
    ObjectPool<Boom> boomPool;
    ObjectPool<MagicArrow> magicArrowPool;
    private void Awake()
    {
        MagicArrowPool.Init();
        MagicBulletPool.Init();
        BoomPool.Init(); 
        magicArrowPool = MagicArrowPool.Instance;
        magicBulletPool = MagicBulletPool.Instance;
        boomPool = BoomPool.Instance;
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
        MagicBullet magicBullet =  magicBulletPool.Get();
        magicBullet.transform.position = playerAttack.transform.position;
        magicBullet.shootByDirection();

        MagicArrow magicArrow = magicArrowPool.Get();
        magicArrow.transform.position = playerAttack.transform.position;
        magicArrow.shootByDirection();

        Boom boom = boomPool.Get();
        boom.transform.position = playerAttack.transform.position;
        boom.shootByDirection();
    }

   private void  RecycleMagicBullet(MagicBullet bullet)
    {
        magicBulletPool.Release(bullet);
    }
    private void RecycleBoom(Boom bullet)
    {
        boomPool.Release(bullet);
    }

    private void RecycleMagicArrow(MagicArrow bullet)
    {
        magicArrowPool.Release(bullet);
    }


}

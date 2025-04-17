using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
[System.Serializable]
public struct Pair
{
    [SerializeReference]
    public MagicBase magic;
    public int count;
}

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private List< Pair> magicEffects= new List< Pair>();
    //magicBullet
    ObjectPool<MagicBullet> magicBulletPool;
    ObjectPool<Boom> boomPool;
    ObjectPool<MagicArrow> magicArrowPool;

    
    Bullet tempBullet;
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

    private void SpawnMagicBullet(PlayerAttack playerAttack, MagicBase magic)
    {

        switch(magic.MagicName)
        {
            case "Ä§·¨×Óµ¯":
                tempBullet = magicBulletPool.Get();
                tempBullet.transform.position = playerAttack.transform.position;
                tempBullet.shootByDirection();
                break;
            case "Ä§·¨¼ý":
                tempBullet = magicArrowPool.Get();
                tempBullet.transform.position = playerAttack.transform.position;
                tempBullet.shootByDirection();
                break;
            case "Õ¨µ¯":
                tempBullet = boomPool.Get();
                tempBullet.transform.position = playerAttack.transform.position;
                tempBullet.shootByDirection();
                break;

        }
        if (magicEffects.Count != 0)
        {
            for (int i = 0; i < magicEffects.Count; i++)
            {
                magicEffects[0].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
            }

        }
    }

   private void  RecycleMagicBullet(MagicBullet bullet)
    {
        magicBulletPool.Release( bullet);
    }
    private void RecycleBoom(Boom bullet)
    {
        boomPool.Release(bullet);
    }

    private void RecycleMagicArrow(MagicArrow bullet)
    {
        magicArrowPool.Release((MagicArrow)bullet);
    }


}

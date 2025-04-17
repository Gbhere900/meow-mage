using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    static BulletPoolManager instance;

    [SerializeField] private List< Pair> magicEffects= new List< Pair>();
    //magicBullet
    ObjectPool<MagicBullet> magicBulletPool;
    ObjectPool<Boom> boomPool;
    ObjectPool<MagicArrow> magicArrowPool;

    
    Bullet tempBullet;

    static public BulletPoolManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(this);
        }
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
 
                magicEffects[i].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
                Pair tempP = magicEffects[i];
                tempP.count --;
                if (tempP.count <= 0)
                {
                    magicEffects.RemoveAt(i);
                }
                else
                {
                    magicEffects[i] = tempP;
                }

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

    public void AddMagicToList(MagicBase magic)
    {
        Pair p = new Pair();
        p.magic = magic;
        p.count = 1;
        magicEffects.Add(p);
    }
}

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
    ObjectPool<T_MagicBullet> T_magicBulletPool;
    ObjectPool<T_MagicArrow> T_magicArrowPool;
    ObjectPool<InstantBoom> instantBoomPool;
    ObjectPool<LightSaber> lightSaberPool;
    ObjectPool<BounceBall> bounceBallPool;
    ObjectPool<BlackHole> blackHolePool;
    ObjectPool<Chainsaw> chainsawPool;
    ObjectPool<EnergyBall> energyBallPool;

    Bullet tempBullet = null;

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
        T_MagicBulletPool.Init();
        T_MagicArrowPool.Init();
        InstantBoomPool.Init();
        LightSaberPool.Init();
        BounceBallPool.Init();
        BlackHolePool.Init();
        ChainsawPool.Init();
        EnergyBallPool.Init();
        magicArrowPool = MagicArrowPool.Instance;
        magicBulletPool = MagicBulletPool.Instance;
        boomPool = BoomPool.Instance;
        T_magicBulletPool = T_MagicBulletPool.Instance;
        T_magicArrowPool = T_MagicArrowPool.Instance;
        instantBoomPool = InstantBoomPool.Instance;
        lightSaberPool = LightSaberPool.Instance;
        bounceBallPool = BounceBallPool.Instance;
        blackHolePool = BlackHolePool.Instance;
        chainsawPool = ChainsawPool.Instance;
        energyBallPool = EnergyBallPool.Instance;
    }
    private void OnEnable()
    {
        //PlayerAttack.OnplayerAttack += SpawnMagicBullet;
        MagicBullet.OnRecycled += RecycleMagicBullet;
        Boom.OnRecycled += RecycleBoom;
        MagicArrow.OnRecycled += RecycleMagicArrow;
        T_MagicBullet.OnRecycled += RecycleT_MagicBullet;
        T_MagicArrow.OnRecycled += RecycleT_MagicArrow;
        InstantBoom.OnRecycled += RecycleInstantBoom;
        LightSaber.OnRecycled += RecycleLightSaber;
        BounceBall.OnRecycled += RecycleBounceBall;
        BlackHole.OnRecycled += RecycleBlackHole;
        Chainsaw.OnRecycled += RecycleChainsaw;
        EnergyBall.OnRecycled += RecycleEnergyBall;
    }
    private void OnDisable()
    {

       // PlayerAttack.OnplayerAttack -= SpawnMagicBullet;
        MagicBullet.OnRecycled -= RecycleMagicBullet;
        Boom.OnRecycled -= RecycleBoom;
        MagicArrow.OnRecycled -= RecycleMagicArrow;
        T_MagicBullet.OnRecycled -= RecycleT_MagicBullet;
        T_MagicArrow.OnRecycled -= RecycleT_MagicArrow;
        InstantBoom.OnRecycled -= RecycleInstantBoom;
        LightSaber.OnRecycled -= RecycleLightSaber;
        BounceBall.OnRecycled -= RecycleBounceBall;
        BlackHole.OnRecycled -= RecycleBlackHole;
        Chainsaw.OnRecycled -= RecycleChainsaw;
        EnergyBall.OnRecycled-= RecycleEnergyBall;
    }

    public  Bullet SpawnMagicBullet(Vector3 position, MagicBase magic,int queueCount, int bulletCount = 1)
    {
        tempBullet = null;
        //for (int j = 0; j < bulletCount; j++)
        //{
        switch (magic.magicSO.ChineseName)
            {
                case "Ä§·¨µ¯":
                    tempBullet = magicBulletPool.Get();
                    //tempBullet.transform.position = position;
                    //tempBullet.shootByMouseDirection();
                    break;
                case "Ä§·¨¼ý":
                    tempBullet = magicArrowPool.Get();
;
                    break;
                case "Õ¨µ¯":
                    tempBullet = boomPool.Get();

                    break;
                case "´¥·¢Ä§·¨µ¯":
                    tempBullet = T_magicBulletPool.Get();

                    break;
                case "´¥·¢Ä§·¨¼ý":
                    tempBullet = T_magicArrowPool.Get();

                    break;
                case "±¬Õ¨Ä§·¨":
                    tempBullet = instantBoomPool.Get();

                    break;
                case "¹â½£":
                    tempBullet = lightSaberPool.Get();

                    break;
                case "µ¯ÌøÄ§·¨µ¯":
                    tempBullet = bounceBallPool.Get();

                    break;
                case "ºÚ¶´":
                    tempBullet = blackHolePool.Get();

                    break;
                case "Á´¾â":
                    tempBullet = chainsawPool.Get();

                    break;
                case "ÄÜÁ¿Çò":
                    tempBullet = energyBallPool.Get();
                break; 

            }
        if (tempBullet)
        {
            tempBullet.transform.position = position;
            tempBullet.shootByMouseDirection();
            tempBullet.queueCount = queueCount;
        }
        if (magicEffects.Count != 0)
            {

                if (magic.magicSO.type == E_MagicType.attack)
                {
                    for (int i=0; i <magicEffects.Count; i++)
                    {

                        magicEffects[i].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
                        tempBullet.shootByMouseDirection();
                        Pair tempP = magicEffects[i];
                        tempP.count--;
                        if (tempP.count <= 0)
                        {
                            magicEffects.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            magicEffects[i] = tempP;
                        }
  
                    }
                }
            }

        return tempBullet;

        //}
    }

    public Bullet SpawnMagicBullet(Vector3 position, Vector3 forward, MagicBase magic, int queueCount, int bulletCount = 1)
    {
        tempBullet = null;
        //for (int j = 0; j < bulletCount; j++)
        //{
        Debug.Log(magic.magicSO.ChineseName);
            switch (magic.magicSO.ChineseName)
            {
                case "Ä§·¨µ¯":
                    tempBullet = magicBulletPool.Get();
                    break;
                case "Ä§·¨¼ý":
                    tempBullet = magicArrowPool.Get();
                    break;
                case "Õ¨µ¯":
                    tempBullet = boomPool.Get();
                    //tempBullet.transform.position = position;
                    //tempBullet.shootByDirection(forward);
                    break;
                case "´¥·¢Ä§·¨µ¯":
                    tempBullet = T_magicBulletPool.Get();
                    break;
                case "´¥·¢Ä§·¨¼ý":
                    tempBullet = T_magicArrowPool.Get();
                    break;
                case "±¬Õ¨Ä§·¨":
                    tempBullet = instantBoomPool.Get();
                    break;
                case "¹â½£":
                    tempBullet = lightSaberPool.Get();
                    break;
                case "µ¯ÌøÄ§·¨µ¯":
                    tempBullet = bounceBallPool.Get();
                    break;
                case "ºÚ¶´":
                    tempBullet = blackHolePool.Get();
                    break;
                case "Á´¾â":
                    tempBullet = chainsawPool.Get();
                    break;
                case "ÄÜÁ¿Çò":
                    tempBullet = energyBallPool.Get();
                    break;
        }


        if (tempBullet)
        {
            tempBullet.transform.position = position;
            tempBullet.shootByDirection(forward);
            tempBullet.queueCount = queueCount;
        }
        if (magicEffects.Count != 0)
            {
                if (magic.magicSO.type == E_MagicType.attack)
                {
                    for (int i = 0; i < magicEffects.Count; i++)
                    {

                        magicEffects[i].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
                        tempBullet.shootByDirection(forward);
                        //if (j == bulletCount - 1)
                        //{
                        Pair tempP = magicEffects[i];
                            tempP.count--;
                            if (tempP.count <= 0)
                            {
                                magicEffects.RemoveAt(i);
                                i--;
                            }
                            else
                            {
                                magicEffects[i] = tempP;
                            }
                       // }
                        
                    }

                }
            }
            
        return tempBullet;

               
                
       // }
    }
    public void AddMagicToList(MagicBase magic)
    {
        Pair p = new Pair();
        p.magic = magic;
        p.count = 1;
        magicEffects.Add(p);
    }

    public void AddEachMagicCount(int addValue)
    {
        for (int i = 0; i < magicEffects.Count; i++)
        {
            Pair tempP = magicEffects[i];
            tempP.count += addValue;
            magicEffects[i] = tempP;
        }
    }

    public void ClearMagicEffect()
    {
        magicEffects.Clear();
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
        magicArrowPool.Release(bullet);
    }
    
    private void RecycleT_MagicBullet(T_MagicBullet bullet)
    {
        T_magicBulletPool.Release(bullet);
    }
    private void RecycleT_MagicArrow(T_MagicArrow bullet)
    {
        T_magicArrowPool.Release(bullet);
    }
    private void RecycleInstantBoom(InstantBoom bullet)
    {
        instantBoomPool.Release(bullet);
    }

    private void RecycleLightSaber(LightSaber bullet)
    {
        lightSaberPool.Release(bullet);
    }

    private void RecycleBounceBall(BounceBall bullet)
    {
        bounceBallPool.Release(bullet);
    }
    private void RecycleBlackHole(BlackHole bullet)
    {
        blackHolePool.Release(bullet);
    }

    private void RecycleChainsaw(Chainsaw bullet)
    {
        chainsawPool.Release(bullet);
    }
    private void RecycleEnergyBall(EnergyBall bullet)
    {
        energyBallPool.Release(bullet);
    }
}

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
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
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
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public  Bullet SpawnMagicBullet(Vector3 position, MagicBase magic,int queueCount, int bulletCount = 1)
    {
        tempBullet = null;
        //for (int j = 0; j < bulletCount; j++)
        //{
        switch (magic.magicSO.ChineseName)
            {
                case "魔法弹":
                    tempBullet = magicBulletPool.Get();
                    //tempBullet.transform.position = position;
                    //tempBullet.shootByMouseDirection();
                    break;
                case "魔法箭":
                    tempBullet = magicArrowPool.Get();
;
                    break;
                case "炸弹":
                    tempBullet = boomPool.Get();

                    break;
                case "触发魔法弹":
                    tempBullet = T_magicBulletPool.Get();

                    break;
                case "触发魔法箭":
                    tempBullet = T_magicArrowPool.Get();

                    break;
                case "爆炸魔法":
                    tempBullet = instantBoomPool.Get();

                    break;
                case "光剑":
                    tempBullet = lightSaberPool.Get();

                    break;
                case "弹跳魔法弹":
                    tempBullet = bounceBallPool.Get();

                    break;
                case "黑洞":
                    tempBullet = blackHolePool.Get();

                    break;
                case "链锯":
                    tempBullet = chainsawPool.Get();

                    break;
                case "能量球":
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
                case "魔法弹":
                    tempBullet = magicBulletPool.Get();
                    break;
                case "魔法箭":
                    tempBullet = magicArrowPool.Get();
                    break;
                case "炸弹":
                    tempBullet = boomPool.Get();
                    //tempBullet.transform.position = position;
                    //tempBullet.shootByDirection(forward);
                    break;
                case "触发魔法弹":
                    tempBullet = T_magicBulletPool.Get();
                    break;
                case "触发魔法箭":
                    tempBullet = T_magicArrowPool.Get();
                    break;
                case "爆炸魔法":
                    tempBullet = instantBoomPool.Get();
                    break;
                case "光剑":
                    tempBullet = lightSaberPool.Get();
                    break;
                case "弹跳魔法弹":
                    tempBullet = bounceBallPool.Get();
                    break;
                case "黑洞":
                    tempBullet = blackHolePool.Get();
                    break;
                case "链锯":
                    tempBullet = chainsawPool.Get();
                    break;
                case "能量球":
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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // 确保场景销毁时清空引用
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // 场景加载后，清空对象池并重新创建
        MagicBulletPool._instance = null;
        MagicArrowPool._instance = null ;
        BoomPool._instance = null;
        T_MagicBulletPool._instance = null;
        T_MagicArrowPool._instance = null ;
        InstantBoomPool._instance = null;
        LightSaberPool._instance = null;
        BounceBallPool._instance = null;
        BlackHolePool._instance = null;
        ChainsawPool._instance = null;
        EnergyBallPool._instance = null;

        //if (_instance != null)
        //{
        //    _instance.Clear(); // 清空池中的所有对象
        //}
    }
}

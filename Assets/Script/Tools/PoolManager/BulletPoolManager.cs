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
        T_MagicBulletPool.Init();
        T_MagicArrowPool.Init();
        magicArrowPool = MagicArrowPool.Instance;
        magicBulletPool = MagicBulletPool.Instance;
        boomPool = BoomPool.Instance;
        T_magicBulletPool = T_MagicBulletPool.Instance;
        T_magicArrowPool = T_MagicArrowPool.Instance;
    }
    private void OnEnable()
    {
        //PlayerAttack.OnplayerAttack += SpawnMagicBullet;
        MagicBullet.OnRecycled += RecycleMagicBullet;
        Boom.OnRecycled += RecycleBoom;
        MagicArrow.OnRecycled += RecycleMagicArrow;
        T_MagicBullet.OnRecycled += RecycleT_MagicBullet;
        T_MagicArrow.OnRecycled += RecycleT_MagicArrow;
    }
    private void OnDisable()
    {

       // PlayerAttack.OnplayerAttack -= SpawnMagicBullet;
        MagicBullet.OnRecycled -= RecycleMagicBullet;
        Boom.OnRecycled -= RecycleBoom;
        MagicArrow.OnRecycled -= RecycleMagicArrow;
        T_MagicBullet.OnRecycled -= RecycleT_MagicBullet;
        T_MagicArrow.OnRecycled -= RecycleT_MagicArrow;
    }

    public  void SpawnMagicBullet(Vector3 position, MagicBase magic,int queueCount)
    {

        switch(magic.magicSO.ChineseName)
        {
            case "Ä§·¨µ¯":
                tempBullet = magicBulletPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByMouseDirection();
                break;
            case "Ä§·¨¼ý":
                tempBullet = magicArrowPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByMouseDirection();
                break;
            case "Õ¨µ¯":
                tempBullet = boomPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByMouseDirection();
                break;
            case "´¥·¢Ä§·¨µ¯":
                tempBullet = T_magicBulletPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByMouseDirection();
                break;
            case "´¥·¢Ä§·¨¼ý":
                tempBullet = T_magicArrowPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByMouseDirection();
                break;
        }
        if (magicEffects.Count != 0)
        {
            
            if(magic.magicSO.type ==E_MagicType.attack) 
            {
                for (int i = magicEffects.Count - 1; i >= 0; i--)
                {

                    magicEffects[i].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
                    Pair tempP = magicEffects[i];
                    tempP.count--;
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
        if (tempBullet)
        tempBullet.queueCount = queueCount;
        tempBullet = null;  
    }

    public void SpawnMagicBullet(Vector3 position,Vector3 forward, MagicBase magic,int queueCount)
    {
        Debug.Log(magic.magicSO.ChineseName);
        switch (magic.magicSO.ChineseName)
        {
            case "Ä§·¨µ¯":
                tempBullet = magicBulletPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByDirection(forward);
                break;
            case "Ä§·¨¼ý":
                tempBullet = magicArrowPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByDirection(forward);
                break;
            case "Õ¨µ¯":
                tempBullet = boomPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByDirection(forward);
                break;
            case "´¥·¢Ä§·¨µ¯":
                tempBullet = T_magicBulletPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByDirection(forward);
                break;
            case "´¥·¢Ä§·¨¼ý":
                tempBullet = T_magicArrowPool.Get();
                tempBullet.transform.position = position;
                tempBullet.shootByDirection(forward);
                break;
        }
        if (magicEffects.Count != 0)
        {

            if (magic.magicSO.type == E_MagicType.attack)
            {
                for (int i = magicEffects.Count - 1; i >= 0; i--)
                {

                    magicEffects[i].magic.GetComponent<I_MagicEffect>().TriggerEffect(tempBullet);
                    Pair tempP = magicEffects[i];
                    tempP.count--;
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
        if(tempBullet)
        tempBullet.queueCount = queueCount;
        tempBullet = null;
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


}

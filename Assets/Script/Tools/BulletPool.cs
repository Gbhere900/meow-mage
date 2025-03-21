using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Pool;
static public class BulletPool
{
    static public ObjectPool<Bullet> bulletPool;
    static  Bullet magicBullet;
     static BulletPool()
    {
        bulletPool = new ObjectPool<Bullet>(CreateFunction_magicBullet, ActionOnGet_magicBullet, ActionOnRelease_magicBullet, ActionOnDestroy_magicBullet);
        magicBullet = Resources.Load<Bullet>("Prefabs/MagicBullet");
        if (magicBullet == null)
            Debug.Log("magicBulletº”‘ÿ ß∞‹");
    }

    private static Bullet CreateFunction_magicBullet()
    {
        return GameObject.Instantiate(magicBullet);
       
    }

    private static void ActionOnGet_magicBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private static void ActionOnRelease_magicBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private static void ActionOnDestroy_magicBullet(Bullet bullet)
    {
        GameObject.Destroy(bullet.gameObject); 
    }

    

    
}

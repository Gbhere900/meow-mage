using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletPool : BasicPoolClass<Bullet>
{
    static MagicBulletPool()
    {
        prefabs = Resources.Load<Bullet>("Prefabs/Bullet/MagicBullet");
    }
    static public void Init()
    {

    }
}

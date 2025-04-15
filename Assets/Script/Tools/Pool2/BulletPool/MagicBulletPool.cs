using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletPool : BasicPoolClass<MagicBullet>
{
    static MagicBulletPool()
    {
        prefabs = Resources.Load<MagicBullet>("Prefabs/Bullet/MagicBullet");
    }
    static public void Init()
    {

    }
}

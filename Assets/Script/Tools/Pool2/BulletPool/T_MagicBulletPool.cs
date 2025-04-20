using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_MagicBulletPool : BasicPoolClass<T_MagicBullet>
{
    // Start is called before the first frame update
    static T_MagicBulletPool()
    {
        prefabs = Resources.Load<T_MagicBullet>("Prefabs/Bullet/T_MagicBullet");
    }
    static public void Init()
    {

    }
}

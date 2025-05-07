using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGelBulletPool : BasicPoolClass<SlimeGelBullet>
{
    static SlimeGelBulletPool()
    {
        prefabs = Resources.Load<SlimeGelBullet>("Prefabs/Bullet/SlimeGelBullet");
    }
    static public void Init()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowPool : BasicPoolClass<Bullet>
{
    static MagicArrowPool()
    {
        prefabs = Resources.Load<Bullet>("Prefabs/Bullet/MagicArrow");
    }
    static public void Init()
    {

    }
}

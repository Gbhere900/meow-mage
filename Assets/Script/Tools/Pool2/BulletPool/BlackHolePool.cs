using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHolePool : BasicPoolClass<BlackHole>
{
    static BlackHolePool()
    {
        prefabs = Resources.Load<BlackHole>("Prefabs/Bullet/BlackHole");
    }
    static public void Init()
    {

    }
}

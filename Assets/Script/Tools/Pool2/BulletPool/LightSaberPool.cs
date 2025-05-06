using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberPool : BasicPoolClass<LightSaber>
{
    static LightSaberPool()
    {
        prefabs = Resources.Load<LightSaber>("Prefabs/Bullet/LightSaber");
    }
    static public void Init()
    {

    }
}

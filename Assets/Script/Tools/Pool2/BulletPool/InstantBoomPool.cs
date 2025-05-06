using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantBoomPool : BasicPoolClass<InstantBoom>
{
    // Start is called before the first frame update
    static InstantBoomPool()
    {
        prefabs = Resources.Load<InstantBoom>("Prefabs/Bullet/InstantBoom");
    }
    static public void Init()
    {

    }

}

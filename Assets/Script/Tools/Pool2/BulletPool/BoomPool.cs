using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPool : BasicPoolClass<Bullet>
{
    // Start is called before the first frame update
    static BoomPool()
    {
        prefabs = Resources.Load<Bullet>("Prefabs/Bullet/Boom");
    }
    static public void Init()
    {

    }

}

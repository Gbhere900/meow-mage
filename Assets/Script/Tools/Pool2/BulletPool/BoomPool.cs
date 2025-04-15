using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPool : BasicPoolClass<Boom>
{
    // Start is called before the first frame update
    static BoomPool()
    {
        prefabs = Resources.Load<Boom>("Prefabs/Bullet/Boom");
    }
    static public void Init()
    {

    }

}

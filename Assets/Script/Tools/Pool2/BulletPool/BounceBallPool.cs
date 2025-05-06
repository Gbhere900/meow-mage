using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBallPool : BasicPoolClass<BounceBall>
{
    static BounceBallPool()
    {
        prefabs = Resources.Load<BounceBall>("Prefabs/Bullet/BounceBall");
    }
    static public void Init()
    {

    }
}

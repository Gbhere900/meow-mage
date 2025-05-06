using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawPool : BasicPoolClass<Chainsaw>
{
    // Start is called before the first frame update
    static ChainsawPool()
    {
        prefabs = Resources.Load<Chainsaw>("Prefabs/Bullet/Chainsaw");
    }
    static public void Init()
    {

    }
}

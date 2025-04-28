using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBallPool : BasicPoolClass<EXPBall>
{
    static  EXPBallPool()
    {
        prefabs = Resources.Load<EXPBall>("Prefabs/Collectable/EXP_Ball");
    }
    public static void Init() { }
}

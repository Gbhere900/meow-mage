using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBallPool : BasicPoolClass<GoldBall>
{
    static GoldBallPool()
    {
        prefabs = Resources.Load<GoldBall>("Prefabs/Gold_ball");
    }
    public static void Init() { }
}

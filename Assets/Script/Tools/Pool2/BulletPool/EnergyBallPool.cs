using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallPool : BasicPoolClass<EnergyBall>
{
    static EnergyBallPool()
    {
        prefabs = Resources.Load<EnergyBall>("Prefabs/Bullet/EnergyBall");
    }
    static public void Init()
    {

    }
}

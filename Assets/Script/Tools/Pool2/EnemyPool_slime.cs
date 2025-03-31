using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool_slime : BasicPoolClass<EnemySpawnSpot>
{ 
    static EnemyPool_slime ()
    {
        prefabs = Resources.Load<EnemySpawnSpot>("Prefabs/EnemySpawnSpot_slime");
    }
    public static void Init() { }
}

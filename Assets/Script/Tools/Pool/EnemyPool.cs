using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : BasicPoolClass<EnemyHealth>
{ 
    static EnemyPool ()
    {
        prefabs = Resources.Load<EnemyHealth>("Prefabs/Slime1");
    }
    public static void Init() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIPool : BasicPoolClass<DamageUI>
{
    // Start is called before the first frame update
    static DamageUIPool()
    {
        prefabs = Resources.Load<DamageUI>("Prefabs/DamageText");
    }
    static public void Init()
    {

    }
}

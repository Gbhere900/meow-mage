using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPool_PassAway : BasicPoolClass<VFX>
{

    static VFXPool_PassAway()
    {
        prefabs = Resources.Load<VFX>("VFX/VFX_PassAway");
    }
    static public void Init()
    {

    }
}

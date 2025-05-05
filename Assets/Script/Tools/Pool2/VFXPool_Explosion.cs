using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPool_Explosion : BasicPoolClass<VFX_Explosion>
{

    static VFXPool_Explosion()
    {
        prefabs = Resources.Load<VFX_Explosion>("Prefabs/VFX/VFX_Explosion");
    }
    static public void Init()
    {

    }
}

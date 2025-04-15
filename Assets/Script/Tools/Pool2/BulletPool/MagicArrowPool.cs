using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowPool : BasicPoolClass<MagicArrow>
{
    static MagicArrowPool()
    {
        prefabs = Resources.Load<MagicArrow>("Prefabs/Bullet/MagicArrow");
    }
    static public void Init()
    {

    }
}

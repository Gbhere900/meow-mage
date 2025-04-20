using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_MagicArrowPool : BasicPoolClass<T_MagicArrow>
{
    // Start is called before the first frame update
    static T_MagicArrowPool()
    {
        prefabs = Resources.Load<T_MagicArrow>("Prefabs/Bullet/T_MagicArrow");
    }
    static public void Init()
    {

    }
}

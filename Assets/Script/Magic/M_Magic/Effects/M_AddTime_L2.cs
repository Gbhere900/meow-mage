using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class M_AddTime_L2 : MagicBase, I_MagicEffect
{
    public float  time = 3;
    public void TriggerEffect(Bullet bullet)
    {
        bullet.SetTime(bullet.time + time);
        
    }
}

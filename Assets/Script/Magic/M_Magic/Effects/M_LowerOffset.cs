
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_LowerAimOffset : MagicBase, I_MagicEffect
{

    public float delta = 10;
    public void TriggerEffect(Bullet bullet)
    {
        bullet.SetAimOffSet(bullet.AimOffset -delta);
        
    }
}

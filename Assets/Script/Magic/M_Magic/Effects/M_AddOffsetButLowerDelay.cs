using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AddOffsetButLowerDelay : MagicBase, I_MagicEffect
{

    public float delta = 60;
    public void TriggerEffect(Bullet bullet)
    {
       bullet.SetAimOffSet(bullet.AimOffset + delta);
       
    }


}

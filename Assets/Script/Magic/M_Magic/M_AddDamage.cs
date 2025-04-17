using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class M_AddDamage : MagicBase, I_MagicEffect
{
    public void TriggerEffect(Bullet bullet)
    {
        bullet.Damage += 20;
        Debug.Log(bullet.Damage);
    }
}

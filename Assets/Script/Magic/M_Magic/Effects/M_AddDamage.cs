using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class M_AddDamage : MagicBase, I_MagicEffect
{
    public int damage = 20;
    public void TriggerEffect(Bullet bullet)
    {
        bullet.Damage += damage;
        Debug.Log(bullet.Damage);
    }
}

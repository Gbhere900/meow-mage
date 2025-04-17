using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AddDamage : MagicBase, I_MagicEffect
{
    public void Trigger(Bullet<MagicBullet> bullet)
    {
        bullet.Damage += 600;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class M_AddSpeed_L1 : MagicBase, I_MagicEffect
{
    public int speed = 5;
    public void TriggerEffect(Bullet bullet)
    {
        bullet.SetSpeed(bullet.Speed+ speed);
        Debug.Log(bullet.Speed);
    }
}

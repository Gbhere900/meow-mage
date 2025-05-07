using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : Bullet
{
    static public Action<EnergyBall> OnRecycled;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }

}

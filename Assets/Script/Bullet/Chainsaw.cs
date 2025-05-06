using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : Bullet
{
    static public Action<Chainsaw> OnRecycled;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }

}
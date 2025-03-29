using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall : Collectable
{
    static public Action<GoldBall> OnRecycled;
    public override void Collect()
    {

        Recycle();
    }
    public void Recycle()
    {
        OnRecycled?.Invoke(this);
    }

}

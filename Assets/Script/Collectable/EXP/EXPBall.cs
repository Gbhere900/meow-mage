using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBall : Collectable
{
    static public Action<EXPBall> OnRecycled;

    public override void Collect()
    {

        Recycle();
    }

    public void Recycle()
    {
        OnRecycled?.Invoke(this);
    }

}

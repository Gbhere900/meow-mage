using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBall : Collectable
{
    static public Action<EXPBall> OnRecycled;
    static public Action OnCollected;

    public override void Collect()
    {
        OnCollected.Invoke();
        Recycle();
    }

    public void Recycle()
    {
        OnRecycled?.Invoke(this);
    }

    

}

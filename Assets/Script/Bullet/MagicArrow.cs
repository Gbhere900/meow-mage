using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : Bullet<MagicArrow>
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

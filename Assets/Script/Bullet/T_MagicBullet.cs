using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_MagicBullet : Bullet
{
    static public Action<T_MagicBullet> OnRecycled;
    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);

        //粒子效果
        //释放另一个法术
        //一些其他效果（音效）
    }
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }
}

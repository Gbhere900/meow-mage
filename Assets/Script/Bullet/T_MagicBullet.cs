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

        //����Ч��
        //�ͷ���һ������
        //һЩ����Ч������Ч��
    }
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Bullet
{
    static public Action<LightSaber> OnRecycled;
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

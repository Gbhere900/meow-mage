using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Bullet
{
    static public Action<Boom> OnRecycled;
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

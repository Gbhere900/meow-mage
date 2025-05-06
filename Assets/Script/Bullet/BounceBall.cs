using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : Bullet
{
    static public Action<BounceBall> OnRecycled;
    protected void Awake()
    {
        base.Awake();
        // ��ȡ����� Rigidbody ���
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }
    protected override void OnTriggerEnter(Collider other)            //super
    {
        base.OnTriggerEnter(other);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<EnemyHealth>() || collision.collider.gameObject.layer == 3)
        {
            if (collision.collider.GetComponent<AIPath>() != null)
            {
                OnCollision?.Invoke(collision.collider.GetComponent<AIPath>());
            }
            collision.collider.GetComponent<EnemyHealth>()?.ReceiveDamage(damage);
            // ��ȡ��һ����ײ��ķ���
            ContactPoint contact = collision.contacts[0];
            Debug.Log("����Ϊ" + contact.normal);
            Vector3 normal = contact.normal;
            normal.y = 0; // �����ߵ� y ������Ϊ 0
            normal = normal.normalized; // ���¹�һ������

            // ���㷴���ٶ�
            Vector3 reflectVelocity = Vector3.Reflect(rigidbody.velocity, normal);
            reflectVelocity.y = 0;
            reflectVelocity = reflectVelocity.normalized;
            // ����������ٶ�
            rigidbody.velocity = reflectVelocity * Speed;
        }
    }

        
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }

}

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
        // 获取物体的 Rigidbody 组件
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
            // 获取第一个碰撞点的法线
            ContactPoint contact = collision.contacts[0];
            Debug.Log("法线为" + contact.normal);
            Vector3 normal = contact.normal;
            normal.y = 0; // 将法线的 y 分量设为 0
            normal = normal.normalized; // 重新归一化法线

            // 计算反射速度
            Vector3 reflectVelocity = Vector3.Reflect(rigidbody.velocity, normal);
            reflectVelocity.y = 0;
            reflectVelocity = reflectVelocity.normalized;
            // 更新物体的速度
            rigidbody.velocity = reflectVelocity * Speed;
        }
    }

        
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }

}

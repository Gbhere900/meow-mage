
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Bullet
{
    public ParticleSystem particleSystem;
    static public Action<BlackHole> OnRecycled;
    Collider[] colliders;
    public int maxCapacity = 50;
    public float radius = 3;
    public float force = 100;
    public float rotationSpeed;
    bool isEnabled = false;
    private void Awake()
    {
        base.Awake();
        colliders = new Collider[maxCapacity];
    }
    protected virtual void OnTriggerEnter(Collider other)            //super
    {
        if (other.GetComponent<EnemyHealth>() != null || other.gameObject.layer == 3)
        {

            other.GetComponent<EnemyHealth>()?.ReceiveDamage(IsCritical() ? Damage * CriticalRatio : Damage);
            if (other.GetComponent<EnemyMove>() != null)
            {
                OnCollision?.Invoke(other.GetComponent<EnemyMove>());
            }
            if(!isEnabled)
            {
                EnableBlackHole();
            }
        }
    }
    protected override void Recycle()
    {
        OnRecycled.Invoke(this);
    }

    public void EnableBlackHole()
    {
        isEnabled = true;
        rigidbody.velocity = Vector3.zero;
        particleSystem.gameObject.SetActive(true);

    }

    private void OnDisable()
    {
        base.OnDisable();
        isEnabled = false;
        particleSystem.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isEnabled)
        {
            int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
            for (int i = 0; i < colliderCount; i++)
            {
                if (colliders[i].gameObject.GetComponent<Rigidbody>() != null)
                {
                    
                    Vector3 direction = transform.position - colliders[i].transform.position;
                    direction.y = 0;
                    colliders[i].gameObject.GetComponent<Rigidbody>().AddForce(direction * force);
                }
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
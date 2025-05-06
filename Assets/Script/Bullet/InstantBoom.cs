using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantBoom : Bullet
{
    [Header("Boom��ֵ")]
    public float timeBeforeExplosion;
    public float radius = 3;
    public int maxCapacity = 50;
    [Header("��Ч")]
    public AudioClip ExplosionAudio;
    Collider[] colliders;
    static public Action<InstantBoom> OnRecycled;

    private void Awake()
    {
        base.Awake();
        colliders = new Collider[maxCapacity];
        
    }
    private void OnEnable()
    {
        
        base.OnEnable();
        StartCoroutine(WaitForExplosion());
        timeBeforeExplosion = time - 0.01f;
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(timeBeforeExplosion);
        Explosion();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void Explosion()
    {
        AudioManager.Instance().PlaySound(ExplosionAudio);
        VFXPoolManager.Instance().SpawnVFX_explosion(transform.position);
        int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].gameObject.GetComponent<EnemyHealth>() !=null)
            {
                // Debug.LogError("ը����" + colliders[i].gameObject.name);
                colliders[i].gameObject.GetComponent<EnemyHealth>().ReceiveDamage(damage);
            }
        }
    }

    
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

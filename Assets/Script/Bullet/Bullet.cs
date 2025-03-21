using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Bullet : MonoBehaviour
{
    [Header("��ֵ")]
    [SerializeField] float speed = 5;
    [SerializeField] float damage = 3;
    [SerializeField] float time = 5;
    [SerializeField] Vector3 aimPosition;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {
        Debug.Log("���"+MousePosition.GetMousePosition());
        Debug.Log("�����"+transform.position);
        SetAimDirection();
        rigidbody.velocity = aimPosition.normalized * speed;   //�����ٶ�
        StartCoroutine(WaitForDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setActive(Boolean active)
    {
        enabled = active;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(EnemyHealth))
        {
            StopCoroutine(WaitForDestroy());
            BulletPool.bulletPool.Release(this);
        }
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(time);
        BulletPool.bulletPool.Release(this );
    }

   public void SetAimDirection()
    {
        aimPosition = (MousePosition.GetMousePosition() - transform.position);
        aimPosition.y = transform.position.y;
        transform.forward = aimPosition.normalized;
    }
}

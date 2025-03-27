using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Bullet : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] float speed = 5;
    [SerializeField] float damage = 3;
    [SerializeField] float time = 5;
    [SerializeField] Vector3 aimPosition;
    private Rigidbody rigidbody;


    static public Action<Bullet> OnRecycled;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {
      //  Debug.Log("鼠标"+MousePosition.GetMousePosition());
       // Debug.Log("发射点"+transform.position);
        StartCoroutine(WaitForDestroy());
       // Debug.Log("方向"+aimPosition);

        
    }

    public void shootByDirection()
    {
        
        SetAimDirection();
        rigidbody.velocity = aimPosition.normalized * speed;  //设置速度
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().ReceiveDamage(damage);
            StopCoroutine(WaitForDestroy());
            BulletPool.bulletPool.Release(this);
        }
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(time);
        OnRecycled.Invoke(this);
    }

   public void SetAimDirection()
    {
        aimPosition = (MousePosition.GetMousePosition() - transform.position);
        aimPosition.y = 0;
        transform.forward = aimPosition.normalized;
    }
}

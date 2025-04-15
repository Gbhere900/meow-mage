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
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float damage = 3;
    [SerializeField] protected float time = 5;
    [SerializeField] protected float CriticalChance = 20;
    [SerializeField] protected float CriticalRatio = 5;
    [SerializeField] protected float ActualDamage = 3;
    [SerializeField] protected Boolean canCutThrough = false;
    [SerializeField] protected Vector3 aimDirection;
    [SerializeField] protected float aimOffset = 10;
    [SerializeField] protected Vector3 shootDirection;
    [SerializeField] protected Boolean isShootByMouseDiretion = true;
    //[SerializeField] Boolean isCharsingEnemy = false;
    protected Rigidbody rigidbody;


    static public Action<Bullet> OnRecycled;
    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {

        if (UnityEngine.Random.Range(0f, 100f) < CriticalChance)
        {
            ActualDamage = damage * CriticalRatio;
        }
        else ActualDamage = damage;
        //  Debug.Log("鼠标"+MousePosition.GetMousePosition());
        // Debug.Log("发射点"+transform.position);
        StartCoroutine(WaitForDestroy());
       // Debug.Log("方向"+aimPosition);

        
    }

    public void shootByDirection()
    {

        if (isShootByMouseDiretion)
        {
            SetAimDirection();
            AddAimOffsetToShootDirection();
        }
        transform.forward = shootDirection.normalized;
        rigidbody.velocity = shootDirection.normalized * speed;  //设置速度
    }
    

     protected virtual void OnTriggerEnter(Collider other)            //super
    {
        if(other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().ReceiveDamage(ActualDamage);
            if(!canCutThrough)
            {
                StopCoroutine(WaitForDestroy());
                OnRecycled.Invoke(this);
            }

        }
    }
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(time);
        OnRecycled.Invoke(this);
    }

   public void SetAimDirection()
    {
        aimDirection = (MousePosition.GetMousePosition() - transform.position);
        aimDirection.y = 0;
        aimDirection = aimDirection.normalized;

    }

    public void AddAimOffsetToShootDirection()
    {
        
        Vector3 offsetVector = new Vector3(-aimDirection.z,0,aimDirection.x).normalized;
        offsetVector *= UnityEngine.Random.Range((float)Math.Tan( -aimOffset* Mathf.Deg2Rad), (float)Math.Tan(aimOffset * Mathf.Deg2Rad));
        shootDirection = aimDirection + offsetVector;

    }
}

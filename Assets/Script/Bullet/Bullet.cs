using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Bullet<T> : MonoBehaviour where T : Bullet<T>
{
    [Header("数值")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float damage = 3;
    [SerializeField] private float time = 5;
    [SerializeField] private float criticalChance = 20;
    [SerializeField] private float criticalRatio = 5;
    [SerializeField] private float actualDamage = 3;
    [SerializeField] private Boolean canCutThrough = false;
    [SerializeField] private Vector3 aimDirection;
    [SerializeField] private float aimOffset = 10;
    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private Boolean isShootByMouseDiretion = true;
    //[SerializeField] Boolean isCharsingEnemy = false;
    protected Rigidbody rigidbody;


    static public Action<T> OnRecycled;

    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Time { get => time; set => time = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public float CriticalRatio { get => criticalRatio; set => criticalRatio = value; }
    public float ActualDamage { get => actualDamage; set => actualDamage = value; }
    public bool CanCutThrough { get => canCutThrough; set => canCutThrough = value; }
    public Vector3 AimDirection { get => aimDirection; set => aimDirection = value; }
    public float AimOffset { get => aimOffset; set => aimOffset = value; }
    public Vector3 ShootDirection { get => shootDirection; set => shootDirection = value; }
    public bool IsShootByMouseDiretion { get => isShootByMouseDiretion; set => isShootByMouseDiretion = value; }

    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {

        if (UnityEngine.Random.Range(0f, 100f) < CriticalChance)
        {
            ActualDamage = Damage * CriticalRatio;
        }
        else ActualDamage = Damage;
        //  Debug.Log("鼠标"+MousePosition.GetMousePosition());
        // Debug.Log("发射点"+transform.position);
        StartCoroutine(WaitForDestroy());
       // Debug.Log("方向"+aimPosition);

        
    }

    public void shootByDirection()
    {

        if (IsShootByMouseDiretion)
        {
            SetAimDirection();
            AddAimOffsetToShootDirection();
        }
        transform.forward = ShootDirection.normalized;
        rigidbody.velocity = ShootDirection.normalized * Speed;  //设置速度
    }
    

     protected virtual void OnTriggerEnter(Collider other)            //super
    {
        if(other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().ReceiveDamage(ActualDamage);
            if(!CanCutThrough)
            {
                StopCoroutine(WaitForDestroy());
                OnRecycled.Invoke((T)this);
            }

        }
    }
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(Time);
        OnRecycled.Invoke((T)this);
    }

   public void SetAimDirection()
    {
        AimDirection = (MousePosition.GetMousePosition() - transform.position);
        Vector3 tempv = new Vector3(AimDirection.x,0,AimDirection.z);
        AimDirection = tempv;
        AimDirection = AimDirection.normalized;

    }

    public void AddAimOffsetToShootDirection()
    {
        
        Vector3 offsetVector = new Vector3(-AimDirection.z,0,AimDirection.x).normalized;
        offsetVector *= UnityEngine.Random.Range((float)Math.Tan( -AimOffset* Mathf.Deg2Rad), (float)Math.Tan(AimOffset * Mathf.Deg2Rad));
        ShootDirection = AimDirection + offsetVector;

    }
}

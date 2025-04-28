using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
abstract public class Bullet : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] private float basicSpeed = 5;
    [SerializeField] private float speed = 5;

    [SerializeField] private float basicDamage = 3;
    [SerializeField] private float damage = 3;

    [SerializeField] private float basicTime = 5;
    [SerializeField] private float time = 5;

    [SerializeField] private float basicCriticalChance = 20;
    [SerializeField] private float criticalChance = 20;

    [SerializeField] private float basicCriticalRatio = 5;
    [SerializeField] private float criticalRatio = 5;

    [SerializeField] private Boolean basicCanCutThrough = false;
    [SerializeField] private Boolean canCutThrough = false;

    [SerializeField] private Boolean isTriggerMagic = false;

    [SerializeField] private Boolean isTriggered = false;

    //[SerializeField] private MagicBase magicToTrigger = null;

    [SerializeField] private Vector3 aimDirection;
    [SerializeField] private float basicAimOffset = 10;
    [SerializeField] private float aimOffset = 10;
    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private Boolean basicIsShootByMouseDiretion = true;
    [SerializeField] private Boolean isShootByMouseDiretion =true;
    //[SerializeField] Boolean isCharsingEnemy = false;
    protected Rigidbody rigidbody;


    static public Action<Bullet> OnRecycled;

  

    private void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {
        Speed = BasicSpeed;
        Damage = BasicDamage;
        Time = BasicTime;
        CriticalChance = BasicCriticalChance;
        CriticalRatio = BasicCriticalRatio;
        CanCutThrough = BasicCanCutThrough;
        AimOffset = BasicAimOffset;
        IsShootByMouseDiretion = BasicIsShootByMouseDiretion;
        isTriggered = false;
        //magicToTrigger = null;

        
        StartCoroutine(WaitForDestroy());

    }

    private void TriggerNextMagic()
    {
        PlayerAttack playerAttack = PlayerAttack.Instance();

           // int tempIndex = playerAttack.magicIndex;
            MagicBase magicToTrigger = null;
            if (playerAttack.MagicQueue.Count >0 )
            {
                if(playerAttack.Mana - playerAttack.MagicQueue.Peek().magicSO.mana >= 0)
                    {
                        magicToTrigger = playerAttack.MagicQueue.Dequeue();
                        playerAttack.Mana -= magicToTrigger.magicSO.mana;
                        // playerAttack.magicIndex++;
                        magicToTrigger.TriggerMagic(transform.position,transform.forward);
                    }
                else
                playerAttack.MagicQueue.Dequeue();

            }
    }

    Boolean IsCritical()
    {
        return UnityEngine.Random.Range(0, 100)<CriticalChance;
    }

    public void shootByMouseDirection()
    {
         SetAimDirection();
         AddAimOffsetToShootDirection();
        transform.forward = ShootDirection.normalized;
        rigidbody.velocity = ShootDirection.normalized * Speed;  //设置速度
    }

    public void shootByDirection(Vector3 forward)
    {
        Vector3 offsetVector = new Vector3(-forward.z, 0, forward.x).normalized;
        offsetVector *= UnityEngine.Random.Range((float)Math.Tan(-AimOffset * Mathf.Deg2Rad), (float)Math.Tan(AimOffset * Mathf.Deg2Rad));
        ShootDirection = forward + offsetVector;
        transform.forward = ShootDirection.normalized;
        rigidbody.velocity = ShootDirection.normalized * Speed;  //设置速度
    }



    protected virtual void OnTriggerEnter(Collider other)            //super
    {
        if(other.GetComponent<EnemyHealth>() != null)
        {
            
            other.GetComponent<EnemyHealth>().ReceiveDamage(IsCritical()?  Damage * CriticalRatio:Damage);

            if (isTriggerMagic && !isTriggered)
            {
                Debug.Log("触发了");
                TriggerNextMagic();
                isTriggered = true;
            }
            

            if(!CanCutThrough)
            {
                StopCoroutine(WaitForDestroy());
                Recycle();
            }

        }
    }
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(Time);
        Recycle();
    }


    abstract protected void Recycle();

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
    public float BasicSpeed { get => basicSpeed; set => basicSpeed = value; }
    public float Speed { get => speed; set => speed = value; }
    public float BasicDamage { get => basicDamage; set => basicDamage = value; }
    public float Damage { get => damage; set => damage = value; }
    public float BasicTime { get => basicTime; set => basicTime = value; }
    public float Time { get => time; set => time = value; }
    public float BasicCriticalChance { get => basicCriticalChance; set => basicCriticalChance = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public float BasicCriticalRatio { get => basicCriticalRatio; set => basicCriticalRatio = value; }
    public float CriticalRatio { get => criticalRatio; set => criticalRatio = value; }
    public bool BasicCanCutThrough { get => basicCanCutThrough; set => basicCanCutThrough = value; }
    public bool CanCutThrough { get => canCutThrough; set => canCutThrough = value; }
    public Vector3 AimDirection { get => aimDirection; set => aimDirection = value; }
    public float BasicAimOffset { get => basicAimOffset; set => basicAimOffset = value; }
    public float AimOffset { get => aimOffset; set => aimOffset = value; }
    public Vector3 ShootDirection { get => shootDirection; set => shootDirection = value; }
    public bool BasicIsShootByMouseDiretion { get => basicIsShootByMouseDiretion; set => basicIsShootByMouseDiretion = value; }
    public bool IsShootByMouseDiretion { get => isShootByMouseDiretion; set => isShootByMouseDiretion = value; }
    public bool IsTriggerMagic { get => isTriggerMagic; set => isTriggerMagic = value; }
    public bool IsTriggered { get => isTriggered; set => isTriggered = value; }
   // public MagicBase MagicToTrigger { get => magicToTrigger; set => magicToTrigger = value; }
}

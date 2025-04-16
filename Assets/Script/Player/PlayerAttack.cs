using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] private float mana;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRecoverSpeed = 10f;
    [SerializeField] private float attackCD = 0.5f;
    [SerializeField] private float reloadCD = 1.0f;
    [SerializeField] float attackTimer;
    //[SerializeField] private Bullet PrefabsToCreat;
    [Header("法术链")]
    public MagicBase magicBullet;
    public MagicBase magicArrow;
    public MagicBase Boom;
    [SerializeField] private List<MagicBase> magicLine = new List<MagicBase>();



    [SerializeField] Boolean canAttack = true ;
  //  Bullet tempBullet;
    [SerializeField] private Transform attackPosition;
    private PlayerInputControl playerInputControl;


    static public Action<PlayerAttack,MagicBase> OnplayerAttack;

    private void Awake()
    {
        magicLine.Add(magicBullet);
        magicLine.Add(magicArrow);
        magicLine.Add(Boom);

        playerInputControl = new PlayerInputControl();

        mana = maxMana;
    }
    private void OnEnable()
    {
        playerInputControl.Enable();
        playerInputControl.Player.Fire.started += Attack;
    }


    void Start()
    {
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer +=Time.deltaTime;
        if(attackTimer > attackCD)
        {
            canAttack = true; 
        }
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
        playerInputControl.Player.Fire.started -= Attack;
    }
   public  void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
        if (canAttack)
        {

            OnplayerAttack.Invoke(this, magicLine[UnityEngine.Random.Range(0,3)]);
            //tempBullet = BulletPool.bulletPool.Get();
            //tempBullet.transform.position =  attackPosition.position;
            //tempBullet.shootByDirection();
            attackTimer = 0;
            canAttack = false;
        }
    }


}

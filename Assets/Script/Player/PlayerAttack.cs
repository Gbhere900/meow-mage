using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    [Header("ÊýÖµ")]
    [SerializeField] private float mana;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRecoverSpeed = 10f;
    [SerializeField] private float attackCD = 0.5f;
    [SerializeField] float attackTimer;
    [SerializeField] private Bullet PrefabsToCreat;


    [SerializeField] Boolean canAttack = true ;
    Bullet tempBullet;
    [SerializeField] private Transform attackPosition;
    private PlayerInputControl playerInputControl;


    static public Action<PlayerAttack> OnplayerAttack;

    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        playerInputControl.Enable();
        playerInputControl.Player.Fire.started += Attack;
        mana = maxMana;
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
    }
    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
        if (canAttack)
        {
            OnplayerAttack.Invoke(this);
            //tempBullet = BulletPool.bulletPool.Get();
            //tempBullet.transform.position =  attackPosition.position;
            //tempBullet.shootByDirection();
            attackTimer = 0;
            canAttack = false;
        }
    }
   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using Microsoft.SqlServer.Server;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    [Header("用户界面")]
    [SerializeField] private UnityEngine.UI.Slider manaSlider;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private UnityEngine.UI.Slider attackSlider;
    [SerializeField] private UnityEngine.UI.Slider reloadSlider;
    [Header("数值")]
    [SerializeField] private float mana;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRecoverSpeed = 10f;
    [SerializeField] private float basicAttackCD = 0.5f;
    [SerializeField] private float attackCD = 0.5f;
    [SerializeField] private float attackTimer = 0;
    [SerializeField] private float basicReloadCD = 1.0f;
    [SerializeField] private float reloadCD = 1.0f;
    [SerializeField] private float reloadTimer = 0;

    //[SerializeField] private Bullet PrefabsToCreat;
    [Header("法术链")]
    public int magicIndex = 0 ;
    public MagicBase magicBullet;
    public MagicBase magicArrow;
    public MagicBase Boom;
    [SerializeField] private List<MagicBase> magicLine = new List<MagicBase>();



    [SerializeField] private Boolean canAttack = true ;
    [SerializeField] private Boolean reloadOver = true;
  //  Bullet tempBullet;
    [SerializeField] private Transform attackPosition;
    private PlayerInputControl playerInputControl;


    static public Action<PlayerAttack,MagicBase> OnplayerAttack;

    private void Awake()
    {
        //magicLine.Add(magicBullet);
        //magicLine.Add(magicArrow);
        //magicLine.Add(Boom);

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
        UpdateTimer();
        UpdateCDSlider();
        UpdateMana();
        UpdateManaSlider();
    }

    private void UpdateMana()
    {
        mana = Math.Min(maxMana, mana + manaRecoverSpeed * Time.deltaTime);
    }

    private void UpdateManaSlider()
    {
        manaSlider.value = mana/maxMana;
        manaText.text = Math.Floor(mana)+ " / "+ Math.Floor(maxMana);
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
        playerInputControl.Player.Fire.started -= Attack;
    }
   public  void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
        if (canAttack && reloadOver)
        {
            canAttack = false;
            attackTimer = 0;
            attackCD = basicAttackCD;
            int i = magicIndex;
            magicIndex++;
            for(;i<magicIndex && i<magicLine.Count ;i++)
            {
                if(mana - magicLine[i].Mana <0 )
                {
                    continue;
                }
                mana -= magicLine[i].Mana;
                magicIndex += magicLine[i].ExtraTrigger;
                attackCD += magicLine[i].AttackCD;
                reloadCD += magicLine[i].ReloadCD;
                OnplayerAttack.Invoke(this, magicLine[i]);
                if (magicLine[i].GetComponent<I_MagicEffect>()!= null)
                {
                    BulletPoolManager.Instance().AddMagicToList(magicLine[i]);

                }
            }
            if (magicIndex >= magicLine.Count)
            {
                magicIndex = 0;

                //重置reload
                reloadOver = false;
                reloadTimer = 0;


            }
        }
    }

    private  void UpdateTimer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCD)
        {
            canAttack = true;
        }
       
        if (!reloadOver)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadCD)
            {
                reloadOver = true;
                reloadCD = basicReloadCD;
            }
        }

    }
    private void UpdateCDSlider()
    {
        attackSlider.value = attackTimer/ attackCD;
        reloadSlider.value = reloadTimer/ reloadCD;
    }


}

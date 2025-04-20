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
using System.ComponentModel;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    static PlayerAttack instance;
    static public PlayerAttack Instance()
    {
        return instance;
    }
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
    public List<MagicBase> magicLine = new List<MagicBase>();
    [SerializeField]  public Queue<MagicBase> magicQueue = new Queue<MagicBase>();



    [SerializeField] private Boolean canAttack = true ;
    [SerializeField] private Boolean reloadOver = true;
  //  Bullet tempBullet;
    [SerializeField] private Transform attackPosition;
    private PlayerInputControl playerInputControl;




    // static public Action<PlayerAttack,MagicBase> OnplayerAttack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //magicLine.Add(magicBullet);
        //magicLine.Add(magicArrow);
        //magicLine.Add(Boom);

        playerInputControl = new PlayerInputControl();

        Mana = MaxMana;
        UpdateMagicQueue();
    }
    private void OnEnable()
    {
        playerInputControl.Enable();
        playerInputControl.Player.Fire.started += Attack;
    }


    void Start()
    {
        AttackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateCDSlider();
        UpdateMana();
        UpdateManaSlider();
        if (magicQueue.Count== 0)
        {
            // magicIndex = 0;
            UpdateMagicQueue();
            //重置reload
            reloadOver = false;
            ReloadTimer = 0;

            BulletPoolManager.Instance().ClearMagicEffect();
            foreach (MagicBase magic in magicQueue)
            {
                Debug.Log(magic.MagicName);
            }
        }
    }

    private void UpdateMana()
    {
        Mana = Math.Min(MaxMana, Mana + ManaRecoverSpeed * Time.deltaTime);
    }

    private void UpdateManaSlider()
    {
        manaSlider.value = Mana/MaxMana;
        manaText.text = Math.Floor(Mana)+ " / "+ Math.Floor(MaxMana);
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
        playerInputControl.Player.Fire.started -= Attack;
    }
   public  void Attack(InputAction.CallbackContext context)
    {
        if (canAttack && reloadOver)
        {
            canAttack = false;
            AttackTimer = 0;
            AttackCD = BasicAttackCD;
            if(Mana - magicQueue.Peek().Mana > 0)
            {
                magicQueue.Dequeue().TriggerMagic(this.transform.position);
            }
            
            //int i = magicIndex;
            //magicIndex++;
            //for(;i<magicIndex && i<magicLine.Count ;i++)
            //{
            //    if(Mana - magicLine[i].Mana <0 )        
            //    {
            //        continue;
            //    }
                //Mana -= magicLine[i].Mana;
                //magicIndex += magicLine[i].ExtraTrigger;
                //BulletPoolManager.Instance().AddEachMagicCount(magicLine[i].ExtraTrigger);

                //AttackCD += magicLine[i].AttackCD;
                //ReloadCD += magicLine[i].ReloadCD;
                //// OnplayerAttack.Invoke(this, magicLine[i]);
                ///

                //magicLine[i].TriggerMagic(this.transform.position);
                


                //if (magicLine[i].GetComponent<I_MagicEffect>()!= null)
                //{
                //    BulletPoolManager.Instance().AddMagicToList(magicLine[i]);
                //}
            
            
        }
    }

    private  void UpdateTimer()
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer > AttackCD)
        {
            AttackTimer = AttackCD;
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
       
        if (!reloadOver)
        {
            ReloadTimer += Time.deltaTime;
            if (ReloadTimer > ReloadCD)
            {
                //ReloadTimer = ReloadCD;
                reloadOver = true;
                ReloadCD = BasicReloadCD;
            }

        }

    }
    private void UpdateCDSlider()
    {
        attackSlider.value = AttackTimer/ AttackCD;
        reloadSlider.value = ReloadTimer/ ReloadCD;
    }
    public void UpdateMagicQueue()
    {
        magicQueue = new Queue<MagicBase>(magicLine);
    }

    public float Mana { get => mana; set => mana = value; }
    public float MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRecoverSpeed { get => manaRecoverSpeed; set => manaRecoverSpeed = value; }
    public float BasicAttackCD { get => basicAttackCD; set => basicAttackCD = value; }
    public float AttackCD { get => attackCD; set => attackCD = value; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public float BasicReloadCD { get => basicReloadCD; set => basicReloadCD = value; }
    public float ReloadCD { get => reloadCD; set => reloadCD = value; }
    public float ReloadTimer { get => reloadTimer; set => reloadTimer = value; }
    public Queue<MagicBase> MagicQueue { get => magicQueue; set => magicQueue = value; }
}

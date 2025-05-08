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
    public HorizontalLayoutGroup magicChainHorizontalLayoutGroup;
    public MagicIcon_disabled MagicIcon_disablePrefabs;
    [SerializeField] private UnityEngine.UI.Slider manaSlider;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private UnityEngine.UI.Slider attackSlider;
    [SerializeField] private UnityEngine.UI.Slider reloadSlider;
    public HorizontalLayoutGroup packageHorizontalLayoutGroup;


    [Header("数值")]
    [SerializeField] private float mana;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRecoverSpeed = 10f;
    [SerializeField] private float basicAttackCD = 0.5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float delayTimer = 0;
    [SerializeField] private float basicReloadCD = 1.0f;
    [SerializeField] private float reload = 1.0f;
    [SerializeField] private float reloadTimer = 0;

    //[SerializeField] private Bullet PrefabsToCreat;
    [Header("法术链")]
    public MagicBase baseMagic;
    public int capacity = 5;
    public int MaxCapacity = 15;
    public int magicIndex = 0 ;
    public List<MagicBase> magicLine = new List<MagicBase>();
    [SerializeField]  public Queue<MagicBase> magicQueue = new Queue<MagicBase>();

    [SerializeField] private Boolean canAttack = true ;
    [SerializeField] private Boolean reloadOver = true;
    [SerializeField] private Transform attackPosition;
    private PlayerInputControl playerInputControl;

    [Header("法术库")]
    public Dictionary<String, MagicBase> magicDic = new Dictionary<String, MagicBase>();

    [Header("重构部分")]
    public int maxQueueCount = 150;
    public List<Queue<MagicBase>> magicQueues = new List<Queue<MagicBase>>(150);
    public int queueCount = -1;


    private void Awake()
    {
        AddMagicEntry("MagicBullet", "M_MagicBullet");
        AddMagicEntry("MagicArrow", "M_MagicArrow");
        AddMagicEntry("Boom", "M_Boom");
        AddMagicEntry("T_MagicBullet", "TM_MagicBullet");
        AddMagicEntry("T_MagicArrow", "TM_MagicArrow");
        AddMagicEntry("InstantBoom", "M_InstantBoom");
        AddMagicEntry("LightSaber", "M_LightSaber");
        AddMagicEntry("BounceBall", "M_BounceBall");
        AddMagicEntry("BlackHole", "M_BlackHole");
        AddMagicEntry("Chainsaw", "M_Chainsaw");
        AddMagicEntry("EnergyBall", "M_EnergyBall");
        AddMagicEntry("2Times", "M_2Times");
        AddMagicEntry("3Times", "M_3Times");
        AddMagicEntry("4Times", "M_4Times");
        AddMagicEntry("AddDamage", "M_AddDamage");
        AddMagicEntry("Stop", "M_Stop");
        AddMagicEntry("FlyAroundPlayer", "M_FlyAroundPlayer");
        AddMagicEntry("FlyAroundEnergyBall", "M_FlyAroundEnergyBall");
        AddMagicEntry("AddSpeed_L1", "M_AddSpeed_L1");
        AddMagicEntry("AddSpeed_L2", "M_AddSpeed_L2");
        AddMagicEntry("AddSpeed_L3", "M_AddSpeed_L3");
        AddMagicEntry("AddTime_L1","M_AddTime_L1");
        AddMagicEntry("AddTime_L2","M_AddTime_L2");


        for (int i = 0; i < maxQueueCount; i++)
        {
            magicQueues.Add(new Queue<MagicBase>());
        }
        for (int i = 0; i < capacity; i++)
        {
           // magicLine.Add(baseMagic);
        }

        if (instance == null)
        {
            instance = this;
        }

        playerInputControl = new PlayerInputControl();

        Mana = MaxMana;
        ReloadMagicQueue();
    }

    


    private void OnEnable()
    {
        playerInputControl.Enable();
        playerInputControl.Player.Fire.started += OnFireTriggered;
    }



    void Start()
    {
        AttackTimer = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerInputControl.Player.Fire.ReadValue<float>() > 0)
        {
            Attack();
        }
    }
    void Update()
    {
        UpdateTimer();
        UpdateCDSlider();
        UpdateMana();
        UpdateManaSlider();
        if (magicQueue.Count== 0)
        {
            // magicIndex = 0;
            ReloadMagicQueue();
            //重置reload
            reloadOver = false;
            ReloadTimer = 0;

            BulletPoolManager.Instance().ClearMagicEffect();
            foreach (MagicBase magic in magicQueue)
            {
                Debug.Log(magic.magicSO.ChineseName);
            }
        }
        ChangeSelectedMagic();
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
        playerInputControl.Player.Fire.started -= OnFireTriggered;
    }

    public void Attack()
    {
        if(magicQueue.Count == magicLine.Count)
            BulletPoolManager.Instance().ClearMagicEffect();

        if (canAttack && reloadOver && Mana - magicQueue.Peek().magicSO.mana > 0)
        {
            canAttack = false;
            AttackTimer = 0;
            AttackCD = BasicAttackCD;
            Queue<MagicBase> tempQueue =  CreateTempQueue();
     
            FillTempQueue(tempQueue);
            magicQueues[queueCount].Dequeue().TriggerMagic(attackPosition.transform.position);
        }
        
    }

    public Queue<MagicBase> CreateTempQueue()
    {
        queueCount++;
        Queue<MagicBase> tempQueue = new Queue<MagicBase>();
        queueCount = queueCount % maxQueueCount;
      //  magicQueues[queueCount] = null;             //防止占用内存
        magicQueues[queueCount] = tempQueue;
        
        return tempQueue;
    }

    public void FillTempQueue(Queue<MagicBase> tempQueue)
    {
        if (magicQueue.Count > 0)
        {
            MagicBase magic = magicQueue.Dequeue();

            magic.queueCount = queueCount;

            if (mana - magic.magicSO.mana < 0)
                magic.isActive = false;
            else
            {
                mana -= magic.magicSO.mana;
                reload += magic.magicSO.reload;
                delay += magic.magicSO.delay;
            }


            tempQueue.Enqueue(magic);

            if (magic.magicSO.isTrigger)
            {
                FillTempQueue(tempQueue);
            }
            for (int i = 0; i < magic.magicSO.extraTrigger; i++)
            {
                FillTempQueue(tempQueue);
            }
        }
    }
    private void OnFireTriggered(InputAction.CallbackContext context)
    {
        Attack();
    }

    public void UpdateMagicLine()
    {
        magicLine.Clear();
        PackageManager package = PackageManager.Instance();
        for(int i=0;i< package.horizontalLayoutGroup.transform.childCount;i++)
        {
            if(package.horizontalLayoutGroup.transform.GetChild(i).childCount >0)
            {
                string magicIdentifier = package.horizontalLayoutGroup.transform.GetChild(i).GetChild(0).GetComponent<MagicIcon>().magicSO.identifier;
                magicLine.Add(magicDic[magicIdentifier]);
            }
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
        else
        {
            ReloadTimer = reload;
        }


    }
    private void UpdateCDSlider()
    {
        attackSlider.value = AttackTimer/ AttackCD;
        reloadSlider.value = ReloadTimer/ ReloadCD;
        
    }
    public void ReloadMagicQueue()
    {

        magicQueue = new Queue<MagicBase>(magicLine);
        for (int i = 0; i < magicChainHorizontalLayoutGroup.transform.childCount; i++)
        {
            Destroy(magicChainHorizontalLayoutGroup.transform.GetChild(i).gameObject);
        }
      
        for(int i=0;i<magicLine.Count;i++)
        {
            MagicIcon_disabled icon = GameObject.Instantiate(MagicIcon_disablePrefabs, magicChainHorizontalLayoutGroup.transform);
            icon.Initialize(magicLine[i].magicSO);
            
        }
        magicQueue = new Queue<MagicBase>(magicLine);
        
    }

    public void ChangeSelectedMagic()
    {
        int currentIndex = magicLine.Count - magicQueue.Count;

        for (int i = 0; i < magicChainHorizontalLayoutGroup.transform.childCount; i++)
        {
            if (i == currentIndex)
            {
                magicChainHorizontalLayoutGroup.transform.GetChild(i).GetComponent<MagicIcon_disabled>().Selected();
            }
            else
            {
                magicChainHorizontalLayoutGroup.transform.GetChild(i).GetComponent<MagicIcon_disabled>().DisSelected();
            }

        }
    }



    private void AddMagicEntry(string key, string objectName)
    {
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogError($"找不到对象: {objectName}");
            return;
        }

        if (obj.TryGetComponent(out MagicBase magic))
        {
            magicDic.Add(key, magic);
        }
        else
        {
            Debug.LogError($"{objectName} 上缺少 MagicBase 组件");
        }
    }
    public float Mana { get => mana; set => mana = value; }
    public float MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRecoverSpeed { get => manaRecoverSpeed; set => manaRecoverSpeed = value; }
    public float BasicAttackCD { get => basicAttackCD; set => basicAttackCD = value; }
    public float AttackCD { get => delay; set => delay = value; }
    public float AttackTimer { get => delayTimer; set => delayTimer = value; }
    public float BasicReloadCD { get => basicReloadCD; set => basicReloadCD = value; }
    public float ReloadCD { get => reload; set => reload = value; }
    public float ReloadTimer { get => reloadTimer; set => reloadTimer = value; }
    public Queue<MagicBase> MagicQueue { get => magicQueue; set => magicQueue = value; }
}

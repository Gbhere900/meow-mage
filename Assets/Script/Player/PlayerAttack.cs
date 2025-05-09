using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    static PlayerAttack instance;
    static public PlayerAttack Instance()
    {
        return instance;
    }
    [Header("用户界面")]
    public HorizontalLayoutGroup magicChainHorizontalLayoutGroup1;//
    public HorizontalLayoutGroup magicChainHorizontalLayoutGroup2;//

    public MagicIcon_disabled MagicIcon_disablePrefabs;//?

    [SerializeField] private UnityEngine.UI.Slider manaSlider;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private UnityEngine.UI.Slider attackSlider;
    [SerializeField] private UnityEngine.UI.Slider reloadSlider;

    public HorizontalLayoutGroup packageHorizontalLayoutGroup1;  //
    public HorizontalLayoutGroup packageHorizontalLayoutGroup2;  //


    [Header("数值")]
    [SerializeField] private float mana;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRecoverSpeed = 10f;
    [SerializeField] private float basicDelayCD = 0.5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float delayTimer = 0;
    [SerializeField] private float basicReloadCD = 1.0f;
    [SerializeField] private float reload = 1.0f;
    [SerializeField] private float reloadTimer = 0;

    //[SerializeField] private Bullet PrefabsToCreat;
    [Header("法术链")]
    public MagicBase baseMagic;  //?
    public int capacity1 = 5;        //
    public int capacity2 = 5;        //

    public int MaxCapacity1 = 15;    //
    public int MaxCapacity2 = 15;    //

    public int magicIndex1 = 0 ;  //
    public int magicIndex2 = 0 ;  //

    public List<MagicBase> magicLine1 = new List<MagicBase>(); //
    public List<MagicBase> magicLine2 = new List<MagicBase>(); //

    [SerializeField]  public Queue<MagicBase> magicQueue1 = new Queue<MagicBase>(); //
    [SerializeField]  public Queue<MagicBase> magicQueue2 = new Queue<MagicBase>(); //

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


    public GameObject MagicWandn1;
    public GameObject MagicWandn2;
    public int queueIndex = 1;
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
        AddMagicEntry("LowerOffset", "M_LowerOffset");
        AddMagicEntry("AddOffsetButLowerDelay", "M_AddOffsetButLowerDelay");


        for (int i = 0; i < maxQueueCount; i++)
        {
            magicQueues.Add(new Queue<MagicBase>());        //?
        }
        for (int i = 0; i < capacity1; i++)
        {
           // magicLine1.Add(baseMagic);                     //
        }
        for (int i = 0; i < capacity2; i++)
        {
            // magicLine2.Add(baseMagic);                     //
        }

        if (instance == null)
        {
            instance = this;
        }

        playerInputControl = new PlayerInputControl();

        Mana = MaxMana;
        ReloadMagicQueue1();                         //
        ReloadMagicQueue2();                         //
    }

    private void OnEnable()
    {
        playerInputControl.Enable();
        playerInputControl.Player.Fire.started += OnFireTriggered;
        playerInputControl.Player.Switch.started += OnSwitchTriggered;
    }


    void Start()
    {
        AttackTimer = 0;
    }

    void Update()
    {
        UpdateTimer();
        UpdateCDSlider();
        UpdateMana();
        UpdateManaSlider();
        if (magicQueue1.Count== 0)               //
        {
            // magicIndex = 0;
            ReloadMagicQueue1();                //
            //重置reload
            reloadOver = false;                 
            ReloadTimer = 0;

            BulletPoolManager.Instance().ClearMagicEffect();
        }
        if (magicQueue2.Count == 0)               //
        {
            // magicIndex = 0;
            ReloadMagicQueue2();                //
            //重置reload
            reloadOver = false;
            ReloadTimer = 0;

            BulletPoolManager.Instance().ClearMagicEffect();
        }
        ChangeSelectedMagic1();                      //
        ChangeSelectedMagic2();                      //
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
        playerInputControl.Player.Switch.started -= OnSwitchTriggered;
    }

    public void Attack()
    {
        if(queueIndex == 1)
        {
            if (magicQueue1.Count == magicLine1.Count)                         //
                BulletPoolManager.Instance().ClearMagicEffect();            //magicEff

            if (canAttack && reloadOver && Mana - magicQueue1.Peek().magicSO.mana > 0)//
            {
                canAttack = false;
                AttackTimer = 0;
                AttackCD = BasicAttackCD;
                Queue<MagicBase> tempQueue = CreateTempQueue();

                FillTempQueue1(tempQueue);
                magicQueues[queueCount].Dequeue().TriggerMagic(attackPosition.transform.position);
            }
        }
        if (queueIndex == 2)
        {
            if (magicQueue2.Count == magicLine2.Count)                         //
                BulletPoolManager.Instance().ClearMagicEffect();            //magicEff

            if (canAttack && reloadOver && Mana - magicQueue2.Peek().magicSO.mana > 0)//
            {
                canAttack = false;
                AttackTimer = 0;
                AttackCD = BasicAttackCD;
                Queue<MagicBase> tempQueue = CreateTempQueue();

                FillTempQueue2(tempQueue);
                magicQueues[queueCount].Dequeue().TriggerMagic(attackPosition.transform.position);
            }
        }

    }

    public void SwitchMagicQueue()
    {
        if (queueIndex == 1)
        {
            queueIndex = 2;
            MagicWandn1.SetActive(false);
            MagicWandn2.SetActive(true);
        }
            
        else if (queueIndex == 2)
        {
            queueIndex = 1;
            MagicWandn1.SetActive(true);
            MagicWandn2.SetActive(false);
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

    public void FillTempQueue1(Queue<MagicBase> tempQueue)
    {
        if (magicQueue1.Count > 0)   //
        {
            MagicBase magic = magicQueue1.Dequeue(); //

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
                FillTempQueue1(tempQueue);
            }
            for (int i = 0; i < magic.magicSO.extraTrigger; i++)
            {
                FillTempQueue1(tempQueue);
            }
        }
    }
    public void FillTempQueue2(Queue<MagicBase> tempQueue)
    {
        if (magicQueue2.Count > 0)   //
        {
            MagicBase magic = magicQueue2.Dequeue(); //

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
                FillTempQueue2(tempQueue);
            }
            for (int i = 0; i < magic.magicSO.extraTrigger; i++)
            {
                FillTempQueue2(tempQueue);
            }
        }
    }

    private void OnFireTriggered(InputAction.CallbackContext context)
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())//在UI上
        {
            Attack();
        }
        else
            GetClickedUIObject();
        
    }

    private GameObject GetClickedUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();

        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            for(int i=0;i<results.Count;i++) 
                Debug.LogWarning(results[i].gameObject.name);
                return results[0].gameObject;
        }

        return null;
    }

    public void UpdateMagicLine1()           //分两个MagicLine
    {
        magicLine1.Clear();  //
        PackageManager package = PackageManager.Instance();
        for(int i=0;i< package.horizontalLayoutGroup1.transform.childCount;i++)  //背包也分两个水平栏
        {
            if(package.horizontalLayoutGroup1.transform.GetChild(i).childCount >0)  //背包也分两个水平栏
            {
                string magicIdentifier = package.horizontalLayoutGroup1.transform.GetChild(i).GetChild(0).GetComponent<MagicIcon>().magicSO.identifier;
                magicLine1.Add(magicDic[magicIdentifier]);  //
            }
        }
    }

        public void UpdateMagicLine2()           //分两个MagicLine
    {
        magicLine2.Clear();  //
        PackageManager package = PackageManager.Instance();
        for(int i=0;i< package.horizontalLayoutGroup2.transform.childCount;i++)  //背包也分两个水平栏
        {
            if(package.horizontalLayoutGroup2.transform.GetChild(i).childCount >0)  //背包也分两个水平栏
            {
                string magicIdentifier = package.horizontalLayoutGroup2.transform.GetChild(i).GetChild(0).GetComponent<MagicIcon>().magicSO.identifier;
                magicLine2.Add(magicDic[magicIdentifier]);  //
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
    public void ReloadMagicQueue1()  //分两个MagicQueue;
    {

        magicQueue1 = new Queue<MagicBase>(magicLine1);  //
        for (int i = 0; i < magicChainHorizontalLayoutGroup1.transform.childCount; i++)  //hori
        {
            Destroy(magicChainHorizontalLayoutGroup1.transform.GetChild(i).gameObject);  
        }
      
        for(int i=0;i<magicLine1.Count;i++)  //
        {
            MagicIcon_disabled icon = GameObject.Instantiate(MagicIcon_disablePrefabs, magicChainHorizontalLayoutGroup1.transform);  //
            icon.Initialize(magicLine1[i].magicSO);     //
            
        }
        magicQueue1 = new Queue<MagicBase>(magicLine1);  //
        
    }

    public void ReloadMagicQueue2()  //分两个MagicQueue;
    {

        magicQueue2= new Queue<MagicBase>(magicLine2);  //
        for (int i = 0; i < magicChainHorizontalLayoutGroup2.transform.childCount; i++)  //hori
        {
            Destroy(magicChainHorizontalLayoutGroup2.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < magicLine2.Count; i++)  //
        {
            MagicIcon_disabled icon = GameObject.Instantiate(MagicIcon_disablePrefabs, magicChainHorizontalLayoutGroup2.transform);  //
            icon.Initialize(magicLine2[i].magicSO);     //

        }
        magicQueue2 = new Queue<MagicBase>(magicLine2);  //

    }


    public void ChangeSelectedMagic1()
    {
        int currentIndex = magicLine1.Count - magicQueue1.Count;    //currentindex也分两个

        for (int i = 0; i < magicChainHorizontalLayoutGroup1.transform.childCount; i++)  //
        {
            if (i == currentIndex)                                      //
            {
                magicChainHorizontalLayoutGroup1.transform.GetChild(i).GetComponent<MagicIcon_disabled>().Selected();//
            }
            else
            {
                magicChainHorizontalLayoutGroup1.transform.GetChild(i).GetComponent<MagicIcon_disabled>().DisSelected();//
            }

        }
    }

    public void ChangeSelectedMagic2()
    {
        int currentIndex = magicLine2.Count - magicQueue2.Count;    //currentindex也分两个

        for (int i = 0; i < magicChainHorizontalLayoutGroup2.transform.childCount; i++)  //
        {
            if (i == currentIndex)                                      //
            {
                magicChainHorizontalLayoutGroup2.transform.GetChild(i).GetComponent<MagicIcon_disabled>().Selected();//
            }
            else
            {
                magicChainHorizontalLayoutGroup2.transform.GetChild(i).GetComponent<MagicIcon_disabled>().DisSelected();//
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


    private void OnSwitchTriggered(InputAction.CallbackContext context)
    {
        SwitchMagicQueue();
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddMagicCapacity1()
    {
        capacity1 = Math.Min(MaxCapacity1, capacity1 + 3);
        PackageManager.Instance().RefreshMagicChain1();
    }

    public void AddMagicCapacity2()
    {
        capacity2 = Math.Min(MaxCapacity2, capacity2 + 3);
        PackageManager.Instance().RefreshMagicChain2();
    }

    public void DecreaseDelay()
    {
        basicDelayCD -= 0.1f;
    }
    public void DecreaseReload()
    {
        basicReloadCD -= 0.2f;
    }


    public void AddManaRecoverSpeed()
    {
        manaRecoverSpeed += 50;
    }

    public void AddMaxMana()
    {
        MaxMana += 200;
    }

    public float Mana { get => mana; set => mana = value; }
    public float MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRecoverSpeed { get => manaRecoverSpeed; set => manaRecoverSpeed = value; }
    public float BasicAttackCD { get => basicDelayCD; set => basicDelayCD = value; }
    public float AttackCD { get => delay; set => delay = value; }
    public float AttackTimer { get => delayTimer; set => delayTimer = value; }
    public float BasicReloadCD { get => basicReloadCD; set => basicReloadCD = value; }
    public float ReloadCD { get => reload; set => reload = value; }
    public float ReloadTimer { get => reloadTimer; set => reloadTimer = value; }
    public Queue<MagicBase> MagicQueue1 { get => magicQueue1; set => magicQueue1 = value; }
    public Queue<MagicBase> MagicQueue2 { get => magicQueue2; set => magicQueue2 = value; }
}

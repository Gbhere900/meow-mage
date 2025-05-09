using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PackageManager : MonoBehaviour
{
    static PackageManager instance;
    [Header("布局")]
    public HorizontalLayoutGroup horizontalLayoutGroup1;
    public HorizontalLayoutGroup horizontalLayoutGroup2;
    public GridLayoutGroup gridLayoutGroup;
    public int MaxGridCapacity = 40;

    [Header("预制体")]
    public MagicIcon MagicIconPrefabs;
    public GameObject beforeIconPrefabs;
    public PackageLib playerPackage;

    [Header("描述")]
    public TextMeshProUGUI description_name;
    public TextMeshProUGUI description_type;
    public Image description_image;
    public TextMeshProUGUI description_description;
    public TextMeshProUGUI description_damage;
    public TextMeshProUGUI description_time;
    public TextMeshProUGUI description_aimOffset;
    public TextMeshProUGUI description_mana;
    public TextMeshProUGUI description_delay;
    public TextMeshProUGUI description_reload;
    static public PackageManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerPackage.CLearPackage();
        ReFreshGrid();

        for(int i = 0;i< MaxGridCapacity ;i++)              //通过MaxGridCapacity限制背包空框的数量
        {
            GameObject beforeIcon = Instantiate(beforeIconPrefabs, gridLayoutGroup.transform);
            beforeIcon.name = "BeforeIcon" + i;
        }
        RefreshMagicChain1();     
        RefreshMagicChain2();     
    }

    public void AddToPackage(MagicSO magicSO)
    {
        playerPackage.AddToList(magicSO);
        ReFreshGrid();
    }

    public MagicIcon CreateNewSlotInGrid(MagicSO magicSO,int SlotNumber)
    {
        MagicIcon magicIcon = Instantiate(MagicIconPrefabs,gridLayoutGroup.transform.GetChild(SlotNumber).transform);
        magicIcon.Initialize(magicSO);
        return magicIcon;
    }
    public MagicIcon CreateNewSlotInHori1(MagicSO magicSO, int SlotNumber)
    {
        MagicIcon magicIcon = Instantiate(MagicIconPrefabs, horizontalLayoutGroup1.transform.GetChild(SlotNumber).transform);
        magicIcon.Initialize(magicSO);
        return magicIcon;
    }

    public MagicIcon CreateNewSlotInHori2(MagicSO magicSO, int SlotNumber)
    {
        MagicIcon magicIcon = Instantiate(MagicIconPrefabs, horizontalLayoutGroup2.transform.GetChild(SlotNumber).transform);
        magicIcon.Initialize(magicSO);
        return magicIcon;
    }

    public void RefreshMagicChain1()
    {
        for (int i = 0; i < horizontalLayoutGroup1.transform.childCount; i++)
        {
            Destroy(horizontalLayoutGroup1.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < PlayerAttack.Instance().capacity1; i++)
        {
            GameObject beforeIcon = Instantiate(beforeIconPrefabs, horizontalLayoutGroup1.transform);
            beforeIcon.name = "BeforeIcon" + i;
        }
        for (int i = 0; i < PlayerAttack.Instance().magicLine1.Count; i++)
        {
            Debug.Log(PlayerAttack.Instance().magicLine1[i].magicSO.ChineseName);
            MagicIcon magicIcon = CreateNewSlotInHori1(PlayerAttack.Instance().magicLine1[i].magicSO, i);
            if (magicIcon == null)
                Debug.LogError("图标为空");
            magicIcon.transform.SetParent(horizontalLayoutGroup1.transform.GetChild(i));
            magicIcon.button.onClick.AddListener(() => ChangeVisualEffect(magicIcon));
            magicIcon.button.onClick.AddListener(() => ChangeDescription(magicIcon.magicSO));
        }
    }

    public void RefreshMagicChain2()
    {
        for (int i = 0; i < horizontalLayoutGroup2.transform.childCount; i++)
        {
            Destroy(horizontalLayoutGroup2.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < PlayerAttack.Instance().capacity2; i++)
        {
            GameObject beforeIcon = Instantiate(beforeIconPrefabs, horizontalLayoutGroup2.transform);
            beforeIcon.name = "BeforeIcon" + i;
        }
        for (int i = 0; i < PlayerAttack.Instance().magicLine2.Count; i++)
        {
            MagicIcon magicIcon = CreateNewSlotInHori2(PlayerAttack.Instance().magicLine2[i].magicSO, i);
            magicIcon.transform.SetParent(horizontalLayoutGroup2.transform.GetChild(i));
            magicIcon.button.onClick.AddListener(() => ChangeVisualEffect(magicIcon));
            magicIcon.button.onClick.AddListener(() => ChangeDescription(magicIcon.magicSO));
        }
    }

    public void ReFreshGrid()
    {
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            if(gridLayoutGroup.transform.GetChild(i).transform.childCount > 0)
            {
                Destroy(gridLayoutGroup.transform.GetChild(i).transform.GetChild(0)?.gameObject);
            }
            
        }

        //当PlayerPackage中法术数量大于MaxGridCapacitys时，默认只显示前背包中MaxGridCapacitys个法术
        for (int i = 0; i < playerPackage.magicSOs.Count && i < MaxGridCapacity; i++)           
        {
            MagicIcon magicIcon = CreateNewSlotInGrid(playerPackage.magicSOs[i],i);
            magicIcon.button.onClick.AddListener(() => ChangeVisualEffect(magicIcon));
            magicIcon.button.onClick.AddListener(() => ChangeDescription(magicIcon.magicSO));
        }
    }


   void ChangeVisualEffect(MagicIcon magicIcon)
    {
        magicIcon.Selected();
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            GameObject beforeIcon = gridLayoutGroup.transform.GetChild(i).gameObject;
            if (beforeIcon.transform.childCount != 0 && beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>() != magicIcon)
            {
                beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>().DisSelected();
            }
        }
        for(int i = 0;i<horizontalLayoutGroup1.transform.childCount; i++)
        {
            GameObject beforeIcon = horizontalLayoutGroup1.transform.GetChild(i).gameObject;
            if (beforeIcon.transform.childCount != 0 && beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>() != magicIcon)
            {
                beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>().DisSelected();
            }
        }
        for (int i = 0; i < horizontalLayoutGroup2.transform.childCount; i++)
        {
            GameObject beforeIcon = horizontalLayoutGroup2.transform.GetChild(i).gameObject;
            if (beforeIcon.transform.childCount != 0 && beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>() != magicIcon)
            {
                beforeIcon.transform.GetChild(0).GetComponent<MagicIcon>().DisSelected();
            }
        }
    }

    void ChangeDescription(MagicSO magicSO)
    {
        description_name.text = magicSO.ChineseName;
        description_type.text = magicSO.type.ToString();
        description_description.text = magicSO.description;
        description_image.sprite = magicSO.icon;
        if (magicSO.bulletSO != null)
        {
            description_damage.text = magicSO.bulletSO.basicDamage.ToString();
            description_time.text = magicSO.bulletSO.basicTime.ToString()+"s";
            description_aimOffset.text = magicSO.bulletSO.basicAimOffset.ToString()+"度";
        }
        else
        {
            description_damage.text = null;
            description_time.text = null;
            description_aimOffset.text = null;
        }
        description_mana.text = magicSO.mana.ToString();
        description_delay.text =  magicSO.delay.ToString()+"s";
        description_reload.text = magicSO.reload.ToString()+"s";
    }


}

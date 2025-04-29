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
    public HorizontalLayoutGroup horizontalLayoutGroup;
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
        RefreshMagicChain();     
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
    public MagicIcon CreateNewSlotInHori(MagicSO magicSO, int SlotNumber)
    {
        MagicIcon magicIcon = Instantiate(MagicIconPrefabs, horizontalLayoutGroup.transform.GetChild(SlotNumber).transform);
        magicIcon.Initialize(magicSO);
        return magicIcon;
    }

    public void RefreshMagicChain()
    {
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            Destroy(horizontalLayoutGroup.transform.GetChild(i));
        }

        for (int i = 0; i < PlayerAttack.Instance().capacity; i++)
        {
            GameObject beforeIcon = Instantiate(beforeIconPrefabs, horizontalLayoutGroup.transform);
            beforeIcon.name = "BeforeIcon" + i;
        }
        for (int i = 0; i < PlayerAttack.Instance().magicLine.Count; i++)
        {
            MagicIcon magicIcon = CreateNewSlotInHori(PlayerAttack.Instance().magicLine[i].magicSO, i);
            magicIcon.transform.SetParent(horizontalLayoutGroup.transform.GetChild(i));
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
        for(int i = 0;i<horizontalLayoutGroup.transform.childCount; i++)
        {
            GameObject beforeIcon = horizontalLayoutGroup.transform.GetChild(i).gameObject;
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
        description_mana.text = "魔力消耗"+ magicSO.mana.ToString();
        description_delay.text = "延迟" + magicSO.delay.ToString();
        description_reload.text = "充能时间" + magicSO.reload.ToString();
    }


}

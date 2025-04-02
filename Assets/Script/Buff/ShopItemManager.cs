using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour

{
    public static ShopItemManager instance;
    
  [SerializeField] List<BasicBuff> buffs = new List<BasicBuff>();
  [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;


    private void Awake()
    {
        if(instance != null&&instance !=this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }
    public static ShopItemManager Instance()
    {
        return instance;
    }

    [NaughtyAttributes.Button]
    public void ChangerShopItem()
    {
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            ShopItemButton tempBuffButton = horizontalLayoutGroup.transform.GetChild(i).GetComponent<ShopItemButton>();
            tempBuffButton.ChangeShopItem();
        }
    }
    public List<BasicBuff> GetBuffs()
    {
        return buffs;
    }


}

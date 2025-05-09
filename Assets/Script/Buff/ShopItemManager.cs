using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour

{
    public static ShopItemManager instance;
    
  [SerializeField] List<BasicBuff> buffLib = new List<BasicBuff>();
  [SerializeField] List<BasicBuff> buffs = new List<BasicBuff>();

  [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    public ShopItemButton shopItemButtonPrefabs;


    private void Awake()
    {
        buffs = buffLib;
        GameManager.OnSwitchGameState += ChangeShopItemsOnSwitchGameState;

        if (instance != null&&instance !=this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
        ChangerShopItems();
    }

    private void ChangeShopItemsOnSwitchGameState(GameState state)
    {
        if(state ==GameState.game)
        {
            ChangerShopItems();
        }
    }

    [NaughtyAttributes.Button]
    public void ChangerShopItems()
    {
        ShuffleBuffs();
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount ; i++)
        {
            Destroy(horizontalLayoutGroup.transform.GetChild(i).gameObject);   
        }
            for (int i = 0; i < 3&& i<buffs.Count; i++)
        {
            ShopItemButton tempShopItemButton = GameObject.Instantiate(shopItemButtonPrefabs, horizontalLayoutGroup.transform);
            tempShopItemButton.ChangeShopItemByBuff(buffs[i]);
            //ShopItemButton tempBuffButton = horizontalLayoutGroup.transform.GetChild(i).GetComponent<ShopItemButton>();

            //tempBuffButton.ChangeShopItemByBuff(buffs[i]);
            buffs[i].count--;
            if(buffs[i].count==0)
            {
                buffs.RemoveAt(i);
            }
            
        }
    }
    public List<BasicBuff> GetBuffs()
    {
        return buffs;
    }

    public void ShuffleBuffs()
    {
        // Fisher-YatesÏ´ÅÆËã·¨
        for (int i = buffs.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (buffs[i],buffs[j]) = (buffs[j], buffs[i]);
        }
       // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
    }

    public static ShopItemManager Instance()
    {
        return instance;
    }
}

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
        GameManager.OnSwitchGameState += ChangeShopItemsOnSwitchGameState;

        if (instance != null&&instance !=this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    private void ChangeShopItemsOnSwitchGameState(GameState state)
    {
        if(state ==GameState.game)
        {
            ChangerShopItems();
        }
    }

    public static ShopItemManager Instance()
    {
        return instance;
    }

    [NaughtyAttributes.Button]
    public void ChangerShopItems()
    {
        ShuffleBuffs();

        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            ShopItemButton tempBuffButton = horizontalLayoutGroup.transform.GetChild(i).GetComponent<ShopItemButton>();
            tempBuffButton.ChangeShopItemByBuff(buffs[i]);
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
            int j = Random.Range(0, i + 1);
            (buffs[i],buffs[j]) = (buffs[j], buffs[i]);
        }
       // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
    }


}

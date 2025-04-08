using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopItemName;
    [SerializeField] private TextMeshProUGUI shopItemDescription;
    [SerializeField] private Image shopItemImage;
    [SerializeField] private Button shopItemButton;
    private ShopItemManager shopItemManager;
    private void Awake()
    {
        shopItemManager = ShopItemManager.Instance();
    }
    public object BuffManager { get; private set; }

    [NaughtyAttributes.Button]
    public void ChangeShopItemRandomly()
    {
        int count = ShopItemManager.Instance().GetBuffs().Count;
        int random = UnityEngine.Random.Range(0, count);
        Debug.Log(random);
        shopItemName.text = ShopItemManager.Instance().GetBuffs()[random].GetName();
        shopItemDescription.text = ShopItemManager.Instance().GetBuffs()[random].GetDescription();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(ShopItemManager.Instance().GetBuffs()[random].ApplyBuff);
    }

    public void ChangeShopItemByIndex(int index)
    {
        //int count = ShopItemManager.Instance().GetBuffs().Count;
        //int random = UnityEngine.Random.Range(0, count);
        Debug.Log(index);
        shopItemName.text = ShopItemManager.Instance().GetBuffs()[index].GetName();
        shopItemDescription.text = ShopItemManager.Instance().GetBuffs()[index].GetDescription();
        if(ShopItemManager.Instance().GetBuffs()[index].GetIcon())
        shopItemImage.sprite = ShopItemManager.Instance().GetBuffs()[index].GetIcon();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(ShopItemManager.Instance().GetBuffs()[index].ApplyBuff);
    }

    public void ChangeShopItemByBuff(BasicBuff buff)
    {
        //int count = ShopItemManager.Instance().GetBuffs().Count;
        //int random = UnityEngine.Random.Range(0, count);
       // Debug.Log(index);
        shopItemName.text = buff.GetName();
        shopItemDescription.text = buff.GetDescription();
        if (buff.GetIcon())
            shopItemImage.sprite = buff.GetIcon();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(buff.ApplyBuff);
    }
}

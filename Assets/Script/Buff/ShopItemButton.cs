using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public void ChangeShopItem()
    {
        int count = ShopItemManager.Instance().GetBuffs().Count;
        int random = UnityEngine.Random.Range(0, count);
        Debug.Log(random);
        shopItemName.text = ShopItemManager.Instance().GetBuffs()[random].GetName();
        shopItemDescription.text = ShopItemManager.Instance().GetBuffs()[random].GetDescription();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(ShopItemManager.Instance().GetBuffs()[random].ApplyBuff);
    }
}

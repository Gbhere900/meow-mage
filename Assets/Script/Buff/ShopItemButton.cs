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

    public void ChangeShopItemByBuff(BasicBuff buff)
    {
        shopItemName.text = buff.GetName();
        shopItemDescription.text = buff.GetDescription();
        if (buff.GetIcon())
            shopItemImage.sprite = buff.GetIcon();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(buff.ApplyBuff);
    }
}

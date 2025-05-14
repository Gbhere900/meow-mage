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
    [SerializeField] private TextMeshProUGUI shopItemCost;
    [SerializeField] private Image shopItemImage;
    [SerializeField] private Button shopItemButton;
    public int cost;
    public bool isValid = true;
    private ShopItemManager shopItemManager;
    private void Awake()
    {
        shopItemManager = ShopItemManager.Instance();
        PlayerResouces.Instance().OnGoldChanged += CheckIfCanBuy;
    }

    private void OnEnable()
    {
        CheckIfCanBuy(PlayerResouces.Instance().GetCurrentGold());
    }
    private void CheckIfCanBuy(int currentGold)
    {
        if(currentGold >= cost && isValid) 
            shopItemButton.interactable = true; 
        else
            shopItemButton.interactable = false;
    }

    public object BuffManager { get; private set; }

    public void ChangeShopItemByBuff(BasicBuff buff)
    {
        cost  = buff.cost;
        shopItemName.text = buff.GetName();
        shopItemDescription.text = buff.GetDescription();
        shopItemCost.text = cost.ToString() + "G";
        if (buff.GetIcon())
            shopItemImage.sprite = buff.GetIcon();
        shopItemButton.onClick.RemoveAllListeners();
        shopItemButton.onClick.AddListener(()=> AudioManager.Instance().PlayBuyAudio());
        shopItemButton.onClick.AddListener(()=> PlayerResouces.Instance().DecreaseGold(cost));
        shopItemButton.onClick.AddListener(()=> shopItemButton.interactable = false);
        shopItemButton.onClick.AddListener(()=> isValid = false);
        shopItemButton.onClick.AddListener(buff.ApplyBuff);
        CheckIfCanBuy(PlayerResouces.Instance().GetCurrentGold());
    }
    
}

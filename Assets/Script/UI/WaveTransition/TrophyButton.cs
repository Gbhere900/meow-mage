using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using DG.Tweening;

public class TrophyButton : MonoBehaviour
{
    public MagicSO magicSO;
    [SerializeField] private TextMeshProUGUI trophyName;
    [SerializeField] private TextMeshProUGUI trophyDescription;
    [SerializeField] private Image trophyImage;
    [SerializeField] private Button trophyButton;
    [SerializeField] private TextMeshProUGUI trophyMana;
    [SerializeField] private TextMeshProUGUI trophyAttackCD;
    [SerializeField] private TextMeshProUGUI trophyReloadCD;
    public bool canBeSelect = true;


    public void ChangeTrophyByMagic(MagicSO magicSO)
    {
        canBeSelect = true;
        transform.DOKill();
        this.magicSO = magicSO;
        if (magicSO.name == null)
        {
            Debug.Log("魔法名为空");
        }
        if (magicSO.description == null)
        {
            Debug.Log("魔法描述为空");
        }
        if (magicSO.icon == null)
        {
            Debug.Log("魔法图标为空");
        }
        //if (magicSO.Mana == null)
        //{
        //    Debug.Log("魔法名为空");
        //}
        //if (magicSO.AttackCD == null)
        //{
        //    Debug.Log("魔法名为空");
        //}
        //if (magicSO.ReloadCD == null)
        //{
        //    Debug.Log("魔法名为空");
        //}

        trophyName.text =magicSO.name;
        trophyDescription.text =magicSO.description;
        trophyImage.sprite = magicSO.icon;
        trophyMana.text = "法力消耗  " +magicSO.mana.ToString();
        trophyAttackCD.text = "延迟  " +magicSO.delay.ToString();
        trophyReloadCD.text = "充能时间  "+magicSO.reload.ToString();

        trophyButton.onClick.AddListener(GetMagic);
    }

    public void  GetMagic()
    {
        PackageManager.Instance().AddToPackage(magicSO);
    }

    public void Selected()
    {
        if (!canBeSelect)
            return;
        canBeSelect = false;
        // 终止该对象上所有正在进行的 DOTween 动画
        transform.DOKill();
        // 创建缩放动画（效果：放大到 1.1 倍，带缓动曲线）
        transform.DOScale(Vector3.one * 1.1f, 0.15f)
            .SetEase(Ease.InOutSine).OnComplete(() =>
            {
                if (PlayerResouces.instance.deltaLevel <= 0)
                {
                    GameManager.instance.SwitchGameState(GameState.wavetransition);
                }
                else
                {
                    GameManager.instance.SwitchGameState(GameState.trophy);
                }
            });
            
    }

    public void DisSelected()
    {
        canBeSelect = false;
        if (!canBeSelect)
            return;
        // 终止该对象上所有正在进行的 DOTween 动画
        transform.DOKill();
        // 创建缩放动画（效果：恢复到原始大小）
        transform.DOScale(Vector3.one, 0.15f);
    }

    public Button GetButton()
    {
        return trophyButton; 
    }
}

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
            Debug.Log("ħ����Ϊ��");
        }
        if (magicSO.description == null)
        {
            Debug.Log("ħ������Ϊ��");
        }
        if (magicSO.icon == null)
        {
            Debug.Log("ħ��ͼ��Ϊ��");
        }
        //if (magicSO.Mana == null)
        //{
        //    Debug.Log("ħ����Ϊ��");
        //}
        //if (magicSO.AttackCD == null)
        //{
        //    Debug.Log("ħ����Ϊ��");
        //}
        //if (magicSO.ReloadCD == null)
        //{
        //    Debug.Log("ħ����Ϊ��");
        //}

        trophyName.text =magicSO.name;
        trophyDescription.text =magicSO.description;
        trophyImage.sprite = magicSO.icon;
        trophyMana.text = "��������  " +magicSO.mana.ToString();
        trophyAttackCD.text = "�ӳ�  " +magicSO.delay.ToString();
        trophyReloadCD.text = "����ʱ��  "+magicSO.reload.ToString();

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
        // ��ֹ�ö������������ڽ��е� DOTween ����
        transform.DOKill();
        // �������Ŷ�����Ч�����Ŵ� 1.1 �������������ߣ�
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
        // ��ֹ�ö������������ڽ��е� DOTween ����
        transform.DOKill();
        // �������Ŷ�����Ч�����ָ���ԭʼ��С��
        transform.DOScale(Vector3.one, 0.15f);
    }

    public Button GetButton()
    {
        return trophyButton; 
    }
}

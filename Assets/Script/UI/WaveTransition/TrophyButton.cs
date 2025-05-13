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
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI aimOffset;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI delay;
    [SerializeField] private TextMeshProUGUI reload;
    public bool canBeSelect = true;


    public void ChangeTrophyByMagicSO(MagicSO magicSO)
    {
        this.magicSO = magicSO;
        if (magicSO.ChineseName == null)
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

        name.text =magicSO.ChineseName;
        description.text =magicSO.description;
        image.sprite = magicSO.icon;
        if (magicSO.bulletSO != null)
        {
            damage.text = magicSO.bulletSO.basicDamage.ToString();
            time.text =   magicSO.bulletSO.basicTime.ToString() + "s";
            aimOffset.text =   magicSO.bulletSO.basicAimOffset.ToString() +"��";
        }
        else
        {
            damage.text = null;
            time.text = null;
            aimOffset.text = null;
        }
        mana.text =   magicSO.mana.ToString();
        delay.text =  magicSO.delay.ToString() + "s";
        reload.text = magicSO.reload.ToString() +"s";

        button.onClick.AddListener(GetMagic);
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
                PlayerResouces.instance.deltaLevel--;
                if (PlayerResouces.instance.deltaLevel <= 0)
                {
                    GameManager.instance.SwitchGameState(GameState.wavetransition);
                }
                else
                {
                    TrophyManager.Instance().ChangeTrophies();
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
        return button; 
    }
}

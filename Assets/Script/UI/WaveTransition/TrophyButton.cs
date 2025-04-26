using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

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


    public void ChangeTrophyByMagic(MagicSO magicSO)
    {
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
        trophyMana.text = magicSO.mana.ToString();
        trophyAttackCD.text = magicSO.delay.ToString();
        trophyReloadCD.text = magicSO.reload.ToString();

        trophyButton.onClick.AddListener(GetMagic);
    }

    public void  GetMagic()
    {
        PackageManager.Instance().AddToPackage(magicSO);
    }

    public void Selected()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.3f).setEase(LeanTweenType.easeInOutSine);
    }
    public void DisSelected()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one , 0.3f);
    }

    public Button GetButton()
    {
        return trophyButton; 
    }
}

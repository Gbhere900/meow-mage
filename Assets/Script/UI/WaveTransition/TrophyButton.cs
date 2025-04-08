using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrophyButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trophyName;
    [SerializeField] private TextMeshProUGUI trophyDescription;
    [SerializeField] private Image trophyImage;
    [SerializeField] private Button trophyButton;
    [SerializeField] private TextMeshProUGUI trophyMana;
    [SerializeField] private TextMeshProUGUI trophyAttackCD;
    [SerializeField] private TextMeshProUGUI trophyReloadCD;

    public void ChangeTrophyByMagic(MagicSO magicSO)
    {
        if(magicSO .MagicName==null)
        {
            Debug.Log("魔法名为空");
        }
        if (magicSO.MagicDescription == null)
        {
            Debug.Log("魔法描述为空");
        }
        if (magicSO.Icon == null)
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

        trophyName.text = magicSO.MagicName;
        trophyDescription.text = magicSO.MagicDescription;
        trophyImage.sprite = magicSO.Icon;
        trophyMana.text = magicSO.Mana.ToString(); 
        trophyAttackCD.text = magicSO.AttackCD.ToString();
        trophyReloadCD.text = magicSO.ReloadCD.ToString();
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

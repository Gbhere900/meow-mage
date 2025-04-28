using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class MagicIcon_disabled : MonoBehaviour
{

    public Image iconImage;
    public Image selectIcon;
    public MagicSO magicSO;
    public bool isSelected = false;



    public MagicIcon_disabled(MagicSO magicSO)
    {
        Initialize(magicSO);
    }
    public void Initialize(MagicSO magicSO)
    {
       this.magicSO = magicSO;
        iconImage.sprite = magicSO.icon;
        selectIcon.enabled = true;
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    public void Selected()
    {
        if(isSelected)
        {
            return;
        }
        isSelected = true;
        selectIcon.gameObject.SetActive(true);
        transform.DOKill();
        // 创建缩放动画（效果：放大到 1.1 倍，带缓动曲线）
        transform.DOScale(Vector3.one * 1.1f, 0.15f)
            .SetEase(Ease.InOutSine);
    }
    public void DisSelected()
    {
        isSelected = false;
        selectIcon.gameObject.SetActive(false);
        transform.DOKill();
        // 创建缩放动画（效果：恢复到原始大小）
        transform.DOScale(Vector3.one, 0.15f);
    }

     
}


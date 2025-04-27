using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class MagicIcon : MonoBehaviour
{

    private PlayerInputControl playerInputControl;

    public Image iconImage;
    public Image selectIcon;
    public Button button;
    public MagicSO magicSO;
    public bool isSelected = false;

    public Transform topOfUiT;
    public Transform beginParentTransform;

    private void OnEnable()
    {
        playerInputControl = new PlayerInputControl();
        playerInputControl.Enable();
    }
    private void OnDisable()
    {
        playerInputControl.Disable();
    }
    public MagicIcon(MagicSO magicSO)
    {
        Initialize(magicSO);
    }
    public void Initialize(MagicSO magicSO)
    {
       this.magicSO = magicSO;
        iconImage.sprite = magicSO.icon;
        selectIcon.enabled = true;
        topOfUiT = GameObject.Find("Canvas").transform;
    }

    public void Selected()
    {
        isSelected = true;
        selectIcon.gameObject.SetActive(true);
    }
    public void DisSelected()
    {
        isSelected = false;
        selectIcon.gameObject.SetActive(false);
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        beginParentTransform = transform.parent;
        transform.SetParent(topOfUiT);
        Image[] subImages = transform.GetComponentsInChildren<Image>();
        foreach (Image subImage in subImages)
        {
           subImage.raycastTarget = false;
        }
    }

    public void OnDrag(BaseEventData eventData)
    {
        transform.position = playerInputControl.Player.MousePosition.ReadValue<Vector2>();

        PointerEventData _ = eventData as PointerEventData;
        Debug.Log(_.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(BaseEventData eventData)
    {
        PointerEventData _ = eventData as PointerEventData;
        if ( _ == null) return;
        GameObject go = _.pointerCurrentRaycast.gameObject;
        if (go.tag == "BeforeIcon")
        {
            SetPosAndParent(go.transform);
        }
        else if(go.tag =="MagicIcon")
        {
            SetPosAndParent(go.transform.parent);
            go.transform.SetParent(topOfUiT);
            if (System.Math.Abs(go.transform.position.x - beginParentTransform.position.x) <= 0) //以下 执行置换动画，完成位置互换 （关于数据的交换，根据自己的工程情况，在下边实现）
            {
                go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                {
                    go.transform.SetParent(beginParentTransform);
                    transform.GetComponent<Image>().raycastTarget = true;
                }).SetEase(Ease.InOutQuint);
            }
            else
            {
                go.transform.DOMoveX(beginParentTransform.position.x, 0.2f).OnComplete(() =>
                {
                    go.transform.DOMoveY(beginParentTransform.position.y, 0.3f).OnComplete(() =>
                    {
                        go.transform.SetParent(beginParentTransform);
                        transform.GetComponent<Image>().raycastTarget = true;
                    }).SetEase(Ease.InOutQuint);
                });
            }

        }
        else
        {
            SetPosAndParent(beginParentTransform);
            Debug.Log("不是ICON");
        }
        PlayerAttack.Instance().UpdateMagicLine();
        PlayerAttack.Instance().ReloadMagicQueue();
        transform.GetComponentInChildren<Image>().raycastTarget = true;
    }

    public void SetPosAndParent(Transform newParent)
    {
        transform.SetParent(newParent) ;
        transform.position = newParent.transform.position;

    }
}


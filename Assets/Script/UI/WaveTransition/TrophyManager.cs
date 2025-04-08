using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrophyManager : MonoBehaviour
{
    [SerializeField] private MagicSO[] magicSOs;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;

    [NaughtyAttributes.Button]
    public void ChangerTrophys()
    {
        ShuffleMagicSOs();
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            TrophyButton tempTrophyButton  = horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>();
            tempTrophyButton.ChangeTrophyByMagic(magicSOs[i]);
        }
    }


    public void ShuffleMagicSOs()
    {
        // Fisher-Yatesϴ���㷨
        for (int i = magicSOs.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (magicSOs[i], magicSOs[j]) = (magicSOs[j], magicSOs[i]);
        }
        // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
    }

}

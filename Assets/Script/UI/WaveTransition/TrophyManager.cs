using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrophyManager : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;

    [NaughtyAttributes.Button]
    public void ChangerTrophys()
    {
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            TrophyButton tempTrophyButton  = horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>();
            tempTrophyButton.ChangeTrophy();
        }
    }

}

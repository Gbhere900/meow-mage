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


    private void Awake()
    {
        GameManager.OnSwitchGameState += ChangeTroPhiesOnSwitchGameState;
    }
    [NaughtyAttributes.Button]
    public void ChangeTrophies()
    {
        ShuffleMagicSOs();
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            TrophyButton tempTrophyButton  = horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>();
            tempTrophyButton.ChangeTrophyByMagic(magicSOs[i]);
            tempTrophyButton.GetButton().onClick.AddListener(() => ChangeVisualEffect(tempTrophyButton));
        }
    }

    private void ChangeVisualEffect(TrophyButton tempTrophyButton)
    {
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            if (tempTrophyButton == horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>())
            {
                tempTrophyButton.Selected();
            }
            else
            {
                tempTrophyButton.DisSelected();
            }
        }

    }

    private void ChangeTroPhiesOnSwitchGameState(GameState gameState)
    {
        if(gameState ==GameState.game)
        {
            ChangeTrophies();
        }
    }


    public void ShuffleMagicSOs()
    {
        // Fisher-YatesÏ´ÅÆËã·¨
        for (int i = magicSOs.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (magicSOs[i], magicSOs[j]) = (magicSOs[j], magicSOs[i]);
        }
        // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
    }

}

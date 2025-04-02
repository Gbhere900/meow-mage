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

    public void ChangeTrophy()
    {
        int count  = Enum.GetValues(typeof(E_TrophyName)).Length;
        int random = UnityEngine.Random.Range(0,count);
        trophyName.text = ((E_TrophyName)random).ToString();
        trophyDescription.text = ((E_TrophyDescription)random).ToString();
    }
}

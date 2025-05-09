using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[System.Serializable]
public class BasicBuff 
{
    [SerializeField] private string name = "XXX";
    [SerializeField] private string description= "XXXXX";
    [SerializeField] private Sprite icon;
    public int count = 1;
    public int cost = 20;
        public  UnityEvent OnApplied;
    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    [NaughtyAttributes.Button]
    public void ApplyBuff()
    {
        OnApplied?.Invoke();
    }
    public Sprite GetIcon()
    {
        return icon;
    }
}

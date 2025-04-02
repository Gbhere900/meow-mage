using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class BasicBuff 
{
    [SerializeField] private string name = "";
    [SerializeField] private string description= "";
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
}

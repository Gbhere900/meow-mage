using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/×Óµ¯Êý¾Ý", order = 0)]
public class BulletSO : ScriptableObject
{
    public float basicSpeed =5;
    public float maxSpeed = 15;
    public float basicDamage = 5;
    public float basicTime = 5;
    public float maxTime  = 15;
    public float basicCriticalChance = 20;
    public float basicCriticalRatio =2;
    public bool basicCanCutThrough = false;
    //public bool isTriggerMagic = false;
    public float basicAimOffset = 10;




}
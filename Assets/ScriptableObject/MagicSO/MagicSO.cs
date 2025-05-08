using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "ScriptableObject/��������", order = 0)]
public class MagicSO : ScriptableObject
{
    public BulletSO bulletSO;

    public string identifier;
    public string ChineseName;
    public string description;
    public Sprite icon;
    public float delay;
    public float reload;
    public int extraTrigger;
    public  float mana;
    public E_MagicType type;
    public bool isTrigger = false;
    public GameObject prefabToCreat;




}

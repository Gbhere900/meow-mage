using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "ScriptableObject/法术数据", order = 0)]
public class MagicSO : ScriptableObject
{
    public string identifier;
    public string name;
    public string description;
    public Sprite icon;
    public float delay;
    public float reload;
    public int extraTrigger;
    public  float mana;
    public E_MagicType type;
    public GameObject prefabToCreat;




}

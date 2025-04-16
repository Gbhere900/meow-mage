using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "ScriptableObject/法术数据", order = 0)]
public class MagicSO : ScriptableObject
{
    [SerializeField] private string magicName;
    [SerializeField] private string magicDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private float attackCD;
    [SerializeField] private float reloadCD;
    [SerializeField] private float mana;
    [SerializeField] private GameObject prefabToCreat;
    [SerializeField] private int i;




    public string MagicName { get => magicName; set => magicName = value; }
    public string MagicDescription { get => magicDescription; set => magicDescription = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public float AttackCD { get => attackCD; set => attackCD = value; }
    public float ReloadCD { get => reloadCD; set => reloadCD = value; }
    public float Mana { get => mana; set => mana = value; }
    public GameObject PrefabToCreat { get => prefabToCreat; set => prefabToCreat = value; }

    //public string MagicType;
}

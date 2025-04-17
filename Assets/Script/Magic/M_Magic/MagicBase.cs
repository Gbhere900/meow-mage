using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicBase : MonoBehaviour
{
    [SerializeField] protected string magicName;
    [SerializeField] protected string magicDescription;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected float attackCD;
    [SerializeField] protected float reloadCD;
    [SerializeField] protected int extraTrigger = 0;
    [SerializeField] protected float mana;
    [SerializeField] protected GameObject prefabToCreat;
    

    public string MagicName { get => magicName; set => magicName = value; }
    public string MagicDescription { get => magicDescription; set => magicDescription = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public float AttackCD { get => attackCD; set => attackCD = value; }
    public float ReloadCD { get => reloadCD; set => reloadCD = value; }
    public float Mana { get => mana; set => mana = value; }
    public GameObject PrefabToCreat { get => prefabToCreat; set => prefabToCreat = value; }
    public int ExtraTrigger { get => extraTrigger; set => extraTrigger = value; }
}

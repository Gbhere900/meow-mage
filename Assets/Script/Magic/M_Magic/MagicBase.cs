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
    [SerializeField] private E_MagicType type;
    [SerializeField] protected GameObject prefabToCreat;
    
    public void TriggerMagic(Vector3 position)
    {
        PlayerAttack playerAttack = PlayerAttack.Instance();
        playerAttack.Mana -= mana;
        playerAttack.AttackCD += attackCD;
        playerAttack.ReloadCD += reloadCD;
        BulletPoolManager.Instance().SpawnMagicBullet(position, this);
        if (Type == E_MagicType.times)
        {
            BulletPoolManager.Instance().AddEachMagicCount(extraTrigger - 1);
        }
        if (GetComponent<I_MagicEffect>() != null)
        {
            BulletPoolManager.Instance().AddMagicToList(this);
        }
        for (int i = 0; i < extraTrigger; i++)
        {
            if (playerAttack.MagicQueue.Count > 0 )
            {
                if(playerAttack.Mana - mana >=0)
                {
                    playerAttack.MagicQueue.Dequeue().TriggerMagic(position);
                }
                else
                {
                    playerAttack.MagicQueue.Dequeue();
                }
            }
        }
       // playerAttack.magicIndex += extraTrigger;
        
        
        
    }

   
    public string MagicName { get => magicName; set => magicName = value; }
    public string MagicDescription { get => magicDescription; set => magicDescription = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public float AttackCD { get => attackCD; set => attackCD = value; }
    public float ReloadCD { get => reloadCD; set => reloadCD = value; }
    public float Mana { get => mana; set => mana = value; }
    public GameObject PrefabToCreat { get => prefabToCreat; set => prefabToCreat = value; }
    public int ExtraTrigger { get => extraTrigger; set => extraTrigger = value; }
    public  E_MagicType Type { get => type; set => type = value; }
}

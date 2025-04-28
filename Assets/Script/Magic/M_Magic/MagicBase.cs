using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicBase : MonoBehaviour
{
    public MagicSO magicSO;

    public void TriggerMagic(Vector3 position)
    {
        PlayerAttack playerAttack = PlayerAttack.Instance();
        playerAttack.Mana -= magicSO.mana;
        playerAttack.AttackCD += magicSO.delay;
        playerAttack.ReloadCD += magicSO.reload;
        BulletPoolManager.Instance().SpawnMagicBullet(position, this);
        if (magicSO.type == E_MagicType.times)
        {
            BulletPoolManager.Instance().AddEachMagicCount(magicSO.extraTrigger - 1);
        }
        if (GetComponent<I_MagicEffect>() != null)
        {
            BulletPoolManager.Instance().AddMagicToList(this);
        }
        for (int i = 0; i < magicSO.extraTrigger; i++)
        {
            if (playerAttack.MagicQueue.Count > 0)
            {
                if (playerAttack.Mana - magicSO.mana >= 0)
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
    public void TriggerMagic(Vector3 position, Vector3 forward)
    {
        PlayerAttack playerAttack = PlayerAttack.Instance();
        playerAttack.Mana -= magicSO.mana;
        playerAttack.AttackCD += magicSO.delay;
        playerAttack.ReloadCD += magicSO.reload;
        BulletPoolManager.Instance().SpawnMagicBullet(position, forward, this);
        if (magicSO.type == E_MagicType.times)
        {
            BulletPoolManager.Instance().AddEachMagicCount(magicSO.extraTrigger - 1);
        }
        if (GetComponent<I_MagicEffect>() != null)
        {
            BulletPoolManager.Instance().AddMagicToList(this);
        }
        for (int i = 0; i < magicSO.extraTrigger; i++)
        {
            if (playerAttack.MagicQueue.Count > 0)
            {
                if (playerAttack.Mana - magicSO.mana >= 0)
                {
                    playerAttack.MagicQueue.Dequeue().TriggerMagic(position, forward);
                }
                else
                {
                    playerAttack.MagicQueue.Dequeue();
                }
            }
        }
    }

}

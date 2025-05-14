using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicBase : MonoBehaviour
{
    public int queueIndex;
    public int queueCount = -1;
    public MagicSO magicSO;
    public bool isActive = true;

    private void OnEnable()
    {
        isActive = true;
    }
    public virtual void TriggerMagic(Vector3 position)
    {
        
        PlayerAttack playerAttack = PlayerAttack.Instance();
        BulletPoolManager.Instance().SpawnMagicBullet(position, this,queueCount);
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
            if (playerAttack.magicQueues[queueCount].Count > 0)
            {
                if (playerAttack.magicQueues[queueCount].Peek().isActive)
                {
                    playerAttack.magicQueues[queueCount].Dequeue().TriggerMagic(position);
                }
                else
                {
                    playerAttack.magicQueues[queueCount].Dequeue();
                }
            }
        }
    }
    public virtual void TriggerMagic(Vector3 position, Vector3 forward)
    {
        PlayerAttack playerAttack = PlayerAttack.Instance();
        BulletPoolManager.Instance().SpawnMagicBullet(position, forward, this,queueCount);
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
            if (playerAttack.magicQueues[queueCount].Count > 0)
            {
                if (playerAttack.magicQueues[queueCount].Peek().isActive)
                {
                      playerAttack.magicQueues[queueCount].Dequeue().TriggerMagic(position,forward);
                }
                else
                {
                    playerAttack.magicQueues[queueCount].Dequeue();  //”–µ„∆Êπ÷
                }
            }
        }
    }

}

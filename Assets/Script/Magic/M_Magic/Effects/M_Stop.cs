using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Stop : MagicBase, I_MagicEffect
{

    public float time = 3;
    public void TriggerEffect(Bullet bullet)
    {
        bullet.OnCollision += AddEffect;
    }

    public void AddEffect(AIPath AiPath)
    {
        if(AiPath != null)
        AiPath.ChangeSpeedForSeconds(0, time);
    }
}

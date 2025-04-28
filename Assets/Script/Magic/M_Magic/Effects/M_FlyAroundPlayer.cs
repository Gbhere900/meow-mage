using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_FlyAroundPlayer : MagicBase, I_MagicEffect
{
    public void TriggerEffect(Bullet bullet)
    {
        FlyAroundPoint flyAroundPoint =  bullet.AddComponent<FlyAroundPoint>();
        flyAroundPoint.centerPoint = GameObject.Find("BulletSpawnPoint").transform.position;
    }
     

}

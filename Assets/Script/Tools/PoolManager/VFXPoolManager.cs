using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPoolManager : MonoBehaviour
{
    static VFXPoolManager instance;
    ObjectPool<VFX_PassAway> VFXPool_passAway;
    ObjectPool<VFX_Explosion> VFXPool_explosion;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
           Destroy(gameObject);

        VFXPool_PassAway.Init();
        VFXPool_Explosion.Init();
        VFXPool_passAway = VFXPool_PassAway.Instance;
        VFXPool_explosion = VFXPool_Explosion.Instance;
    }

    static public VFXPoolManager Instance()
    {
        return instance;
    }
    private void OnEnable()
    {
        EnemyHealth.OnPassAway += SpawnVFX_passAway;
        VFX_PassAway.OnRecycled += RecycleVFX_passAway;

        VFX_Explosion.OnRecycled += RecycleVFX_explosion;
    }
    private void OnDisable()
    {
        EnemyHealth.OnPassAway -= SpawnVFX_passAway;
        VFX_PassAway.OnRecycled -= RecycleVFX_passAway;

        VFX_Explosion.OnRecycled -=RecycleVFX_explosion;
    }

    public void  SpawnVFX_passAway(Vector3 position)
    {
        VFX_PassAway tempVFX = VFXPool_passAway.Get();
        tempVFX.transform.position = position;
    }

    public  void RecycleVFX_passAway(VFX_PassAway tempVFX)
    {
        VFXPool_passAway.Release(tempVFX);
    }

    public void SpawnVFX_explosion(Vector3 position)
    {
        VFX_Explosion tempVFX = VFXPool_explosion.Get();
        tempVFX.transform.position = position;
    }

   public  void RecycleVFX_explosion(VFX_Explosion tempVFX)
    {
        VFXPool_explosion.Release(tempVFX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPoolManager : MonoBehaviour
{
    ObjectPool<VFX> VFXPool_passAway;
    private void Awake()
    {
        VFXPool_PassAway.Init();
        VFXPool_passAway = VFXPool_PassAway.Instance;
    }
    private void OnEnable()
    {
        EnemyHealth.OnPassAway += SpawnVFX_passAway;
        VFX.OnRecycled += RecycleVFX_passAway;
    }
    private void OnDisable()
    {
        EnemyHealth.OnPassAway -= SpawnVFX_passAway;
        VFX.OnRecycled -= RecycleVFX_passAway;
    }

    private void  SpawnVFX_passAway(Vector3 position)
    {
        VFX tempVFX = VFXPool_passAway.Get();
        tempVFX.transform.position = position;
    }

    private void RecycleVFX_passAway(VFX tempVFX)
    {
        VFXPool_passAway.Release(tempVFX);
    }
}

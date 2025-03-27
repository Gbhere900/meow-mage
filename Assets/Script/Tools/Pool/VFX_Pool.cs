using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
static public class VFX_Pool
{
    static public ObjectPool<ParticleSystem> VFX_PassAwayPool;
    static ParticleSystem VFX_PassAway;
    static VFX_Pool()
    {
        VFX_PassAwayPool = new ObjectPool<ParticleSystem>(CreateFunction_passAway, ActionOnGet_passAway, ActionOnRelease_passAway, ActionOnDestroy_passAway);
        VFX_PassAway = Resources.Load<ParticleSystem>("VFX/VFX_PassAway");
        if (VFX_PassAway == null)
            Debug.Log("ParticleSystemº”‘ÿ ß∞‹");
    }

    private static ParticleSystem CreateFunction_passAway()
    {
        return GameObject.Instantiate(VFX_PassAway);

    }

    private static void ActionOnGet_passAway(ParticleSystem VFX_PassAway)
    {
        VFX_PassAway.gameObject.SetActive(true);
    }

    private static void ActionOnRelease_passAway(ParticleSystem VFX_PassAway)
    {
        VFX_PassAway.gameObject.SetActive(false);
    }
    private static void ActionOnDestroy_passAway(ParticleSystem VFX_PassAway)
    {
        GameObject.Destroy(VFX_PassAway.gameObject);
    }

}

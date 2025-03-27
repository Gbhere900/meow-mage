using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BasicPoolClass_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

 abstract public class BasicPoolClass_2<T> where T : Component
{
    static public ObjectPool<T> VFX_PassAwayPool;
    static T prefabs;
    static BasicPoolClass_2()
    {
        VFX_PassAwayPool = new ObjectPool<T>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);

        if (prefabs == null)
            Debug.Log("ParticleSystemº”‘ÿ ß∞‹");
    }

    private static T CreateFunction()
    {
        return GameObject.Instantiate(prefabs);

    }

    private static void ActionOnGet(T prefabs)
    {
        prefabs.gameObject.SetActive(true);
    }

    private static void ActionOnRelease(T prefabs)
    {
        prefabs.gameObject.SetActive(false);
    }
    private static void ActionOnDestroy(T prefabs)
    {
        GameObject.Destroy(prefabs.gameObject);
    }

    protected virtual string GetPrebabsPath()
    {
        return "11111111";
    }
    
}

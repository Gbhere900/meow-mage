
using UnityEngine;
using UnityEngine.Pool;
  abstract public class BasicPoolClass<T> where T : Component
{
    //static protected String prefabsPath;
    static protected T prefabs;
    static private ObjectPool<T> _instance;

    static public ObjectPool<T> Instance { 
        get 
        {  
            if(_instance == null)
            {
              //  prefabs = Resources.Load<T>(prefabsPath);
                //if(prefabsPath == null )
                //{
                //    Debug.Log("预制体路径未找到");
                //    return null;
                //}
                if(prefabs == null )
                {
                    Debug.Log(typeof(T).Name+   "预制体为空");
                }
                _instance = new ObjectPool<T>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
            }
            
             return _instance; 
        }


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

    // protected abstract  String GetPrefabsPath();

    private void OnDestroy()
    {
        if (_instance !=null)
        {
            _instance = null; // 确保场景销毁时清空引用
        }
    }
}


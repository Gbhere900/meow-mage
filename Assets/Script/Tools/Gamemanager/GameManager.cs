using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitchWaveCallBack(int deltaLevel)
    {
        Debug.Log("打开商店");
        for (int i = 0; i < deltaLevel; i++)
        {
            Debug.Log("打开奖励");
        }
    }
}

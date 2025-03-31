using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public Action<GameState> OnSwitchGameState;
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

    public void OnSwitchWaveCallBack()
    {
        int deltaLevel = UnityEngine.Object.FindObjectOfType<PlayerResouces>().GetDeltaLevel();
        SwitchState(GameState.shop);
        for (int i = 0; i < deltaLevel; i++)
        {
            SwitchState(GameState.trophy);
        }
    }

    public void SwitchState(GameState gameState)
    {
        OnSwitchGameState(gameState);
    }
}

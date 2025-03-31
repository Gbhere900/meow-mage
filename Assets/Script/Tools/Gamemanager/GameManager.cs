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

        SwitchGameState(GameState.menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitchWaveCallBack()
    {
        int deltaLevel = UnityEngine.Object.FindObjectOfType<PlayerResouces>().GetDeltaLevel();
        //SwitchGameState(GameState.shop);
        for (int i = 0; i < deltaLevel; i++)
        {
            SwitchGameState(GameState.trophy);
        }
    }

    public void SwitchGameState(GameState gameState)
    {
        OnSwitchGameState(gameState);
    }

    public void GameStart()
    {
        SwitchGameState(GameState.game);
    }
}

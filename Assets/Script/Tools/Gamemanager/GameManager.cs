using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public Action<GameState> OnSwitchGameState;
    public GameState gameState;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
            instance = this;

        SwitchToMenu();
    }
    static public GameManager Instance()
    {
        return instance;
    }
    // Update is called once per frame

    public void OnSwitchWaveCallBack()
    {
       // int deltaLevel = UnityEngine.Object.FindObjectOfType<PlayerResouces>().GetDeltaLevel();
        //SwitchGameState(GameState.shop);
        //for (int i = 0; i < deltaLevel; i++)
        //{
        //    SwitchGameState(GameState.trophy);
        //}
    }

    public void SwitchGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnSwitchGameState.Invoke(gameState);
        Debug.Log(gameState.ToString());
    }

    public void SwitchToMenu()
    {
        SwitchGameState(GameState.menu);
    }

    public void SwitchToGame()
    {
        SwitchGameState(GameState.game);
    }
    public void SwitchToWaveTransition()
    {
        SwitchGameState(GameState.wavetransition);
    }
    public void SwitchToBag()
    {
        Debug.Log("ÇÐ»»µ½bag");
        SwitchGameState(GameState.bag);
    }
    public void SwitchToThophy()
    {
        SwitchGameState(GameState.trophy);
    }
    public void SwitchToShop()
    {
        SwitchGameState(GameState.shop);
    }
    public void SwitchToGameOver()
    {
        SwitchGameState(GameState.gameOver);
    }
    public void SwitchToVictory()
    {
        SwitchGameState(GameState.victory);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
}

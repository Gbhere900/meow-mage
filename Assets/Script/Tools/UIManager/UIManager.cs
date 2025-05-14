using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    public  GameObject gameUI;
    [SerializeField] private GameObject waveTransitionUI;
    [SerializeField] private GameObject trophyUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject packageUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject PauseUI;

    static public UIManager instance;
     
    static public UIManager Instancce()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        GameManager.OnSwitchGameState += SwitchUIState;

        GameManager.OnPaused += OnPauseTriggered;
        GameManager.OnContinued += OnContinueTriggered;
    }



    private void OnDisable()
    {
        GameManager.OnSwitchGameState -= SwitchUIState;

        GameManager.OnPaused -= OnPauseTriggered;
        GameManager.OnContinued -= OnContinueTriggered;
    }

    public void OnPauseTriggered()
    {
        PauseUI.SetActive(true);
    }
    private void OnContinueTriggered()
    {
        PauseUI.SetActive(false);
    }

    //public void SwitchPauseUI()
    //{

    //    if (PauseUI.activeInHierarchy == true)
    //    {
    //        PauseUI.SetActive(false);
    //    }
    //    else
    //    {
    //        PauseUI.SetActive(true);
    //    }
    //}

    private void SwitchUIState(GameState gameState)
    {
        SetAllUIFalse();
        switch (gameState)
        {
            case GameState.menu:
                menuUI.SetActive(true);
                gameUI.SetActive(true); 
                break;
            case GameState.game:
                gameUI.SetActive(true);
                break;
            case GameState.wavetransition:
                waveTransitionUI.SetActive(true);
                gameUI.SetActive(true);
                break;
            case GameState.bag:
                Debug.Log("UImanager«–ªªµΩbag");
                packageUI.SetActive(true);
                break;
            case GameState.trophy:
                trophyUI.SetActive(true);
                break;

            case GameState.shop:
                shopUI.SetActive(true);
                break;
            case GameState.victory:
                victoryUI.SetActive(true);
                break;
            case GameState.gameOver:
                gameOverUI.SetActive(true);
                break;
        }
    }
    void SetAllUIFalse()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        waveTransitionUI.SetActive(false);
        trophyUI.SetActive(false);
        shopUI.SetActive(false);
        packageUI.SetActive(false);
        victoryUI.SetActive(false);
        gameOverUI.SetActive(false);

    }
}

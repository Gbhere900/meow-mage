using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject waveTransitionUI;
    [SerializeField] private GameObject trophyUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject packageUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    
    private void OnEnable()
    {
        GameManager.OnSwitchGameState += SwitchUIState;
    }
    private void OnDisable()
    {
        GameManager.OnSwitchGameState -= SwitchUIState;
    }

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

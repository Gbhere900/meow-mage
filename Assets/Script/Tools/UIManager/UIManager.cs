using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject trophyUI;
    [SerializeField] private GameObject shopUI;
    
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
                Debug.Log("UImanagerÇÐ»»shop×´Ì¬");
                break;
            case GameState.game:
                gameUI.SetActive(true);
                Debug.Log("UImanagerÇÐ»»trophy×´Ì¬");
                break;
            case GameState.trophy:
                trophyUI.SetActive(true);
                Debug.Log("UImanagerÇÐ»»trophy×´Ì¬");
                break;

            case GameState.shop:
                shopUI.SetActive(true);
                Debug.Log("UImanagerÇÐ»»shop×´Ì¬");

                break;
        }
    }
    void SetAllUIFalse()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        trophyUI.SetActive(false);
        shopUI.SetActive(false);
    }
}

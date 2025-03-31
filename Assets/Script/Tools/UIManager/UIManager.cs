using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
        switch (gameState)
        {
            case GameState.shop:
                Debug.Log("UImanager�л�shop״̬");
                break;
            case GameState.trophy:
                Debug.Log("UImanager�л�trophy״̬");
                    break;
        }
    }
}

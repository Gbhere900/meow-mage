using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    public PlayerHealth playerHealth;
    [SerializeField] private PlayerController playerController;
    private void OnEnable()
    {
        GameManager.OnSwitchGameState += ChangeKeyBoardActive;
        playerAttack.enabled = false;
        playerController.enabled = false;
    }

    private void OnDisable()
    {
        GameManager.OnSwitchGameState -= ChangeKeyBoardActive;
    }
    private void ChangeKeyBoardActive(GameState state)
    {
        if(state == GameState.game)
        {
            playerAttack.enabled = true;
            playerController.enabled = true;
        }
        else
        {
            playerAttack.enabled = false;
            playerController.enabled = false;
            
        }
    }

    //public void Add2maxHealth()
    //{
    //    playerHealth.Add5maxHealth();
    //}

    //public void RecoverAllHealth()
    //{
    //    playerHealth.RecoverAllHealth();
    //}

}

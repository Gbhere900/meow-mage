using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private void Awake()
    {
        health = maxHealth;
        UpdateHealthUI();
    }

    public void ReceiverDamage(float damage)
    {
        health -= Math.Min(health, damage);
        UpdateHealthUI();
        if (health == 0)
        {
            GameManager.instance.SwitchGameState(GameState.gameOver);
        }
        
    }
 
    private void UpdateHealthUI()
    {
        healthSlider.value = health / maxHealth;
        healthText.text = health + " / " + maxHealth;
    }
}

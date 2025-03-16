using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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
            SceneManager.LoadScene(0);
        }
        
    }
 
    private void UpdateHealthUI()
    {
        healthSlider.value = health / maxHealth;
        healthText.text = health + " / " + maxHealth;
    }
}

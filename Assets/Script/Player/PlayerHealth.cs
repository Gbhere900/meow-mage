using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private Slider healthSlider;

    private void Awake()
    {
        health = maxHealth;
        healthSlider.value = health / maxHealth;
    }

    public void ReceiverDamage(float damage)
    {
        health -= Math.Min(health, damage);
        healthSlider.value = health / maxHealth;
    }
    
}

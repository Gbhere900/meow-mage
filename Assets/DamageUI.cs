using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float animationPlayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        PlayDamageUIAnimation();
    }

   public void PlayDamageUIAnimation()
    {
        animator.speed = 1/animationPlayTime;
        animator.Play("TextJump");
    }
}

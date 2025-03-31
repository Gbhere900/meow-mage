using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] float animationPlayTime;


    static public Action<DamageUI> OnRecycled; 
    private void OnEnable()
    {
        StartCoroutine(waitForDestroy());
    }

    public void Spawn(float text)
    {
        SetText( text.ToString());
        PlayDamageUIAnimation(animationPlayTime);
    }

    public void SetText(System.String text)
    {
        textMeshPro.text = text;
    }
    public void PlayDamageUIAnimation(float tempAnimationPlayTime)
    {
        
        animator.speed = 1/tempAnimationPlayTime;
        animator.Play("TextJump");
    }

    
    public void PlayDamageUIAnimation()
    {
        
        animator.speed = 1 / animationPlayTime;
        animator.Play("TextJump");
    }



    IEnumerator waitForDestroy()
    {
        yield return new WaitForSeconds(animationPlayTime);
        OnRecycled.Invoke(this);
    }

}

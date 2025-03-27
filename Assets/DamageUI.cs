using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] float animationPlayTime;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(waitForDestroy());
    }

   public void PlayDamageUIAnimation(System.String  s,float tempAnimationPlayTime)
    {
        textMeshPro.text = s;
        animator.speed = 1/tempAnimationPlayTime;
        animator.Play("TextJump");
    }

    public void PlayDamageUIAnimation(float s)
    {
        textMeshPro.text = s.ToString();
        animator.speed = 1 / animationPlayTime;
        animator.Play("TextJump");
    }



    IEnumerator waitForDestroy()
    {
        yield return new WaitForSeconds(animationPlayTime);
        TMP_Pool.TMP_damageTextPool.Release(this);
    }

}

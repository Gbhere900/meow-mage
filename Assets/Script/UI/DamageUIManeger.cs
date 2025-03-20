using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageUIManeger : MonoBehaviour
{
    [SerializeField] GameObject damageText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EnemyHealth.OnReceivedDamage += GenerateDamageUIPrefabs;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EnemyHealth.OnReceivedDamage -= GenerateDamageUIPrefabs;
    }
    void GenerateDamageUIPrefabs(float damage,Vector3 position)
    {
        position.y += 10;
        DamageUI damageUI = GameObject.Instantiate(damageText, position, damageText.transform.rotation).GetComponent<DamageUI>();
        damageUI.PlayDamageUIAnimation(damage);
    }

}

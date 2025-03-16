using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damage = 10;
    [SerializeField] private float force = 1;
    [SerializeField] private float attackSpacing = 1;
    [SerializeField] private float attackTimer = 0;

    private void Awake()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Player" && attackTimer <0)
        {
            Debug.Log("Íæ¼ÒÊÜµ½" + damage);
            attackTimer = attackSpacing;
            AddAttackForce(collision.collider.gameObject);
            collision.collider.GetComponent<PlayerHealth>()?.ReceiverDamage(damage);
        }
    }
    void Update()
    {
        attackTimer -=Time.deltaTime;  
    }

    private void AddAttackForce(GameObject collider)
    {
        collider.GetComponent<Rigidbody>()?.AddForce(force * collider.transform.position - transform.position, ForceMode.Impulse);
    }
}

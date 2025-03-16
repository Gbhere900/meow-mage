using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMove : MonoBehaviour
{
    private Player player;
    private Rigidbody rigidbody;
    [SerializeField] private ParticleSystem VFX_passaway;
    private Vector3 targetDirection;

    public float speed = 2;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (player.transform.position - transform.position).normalized;
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = targetDirection * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "Player")
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        VFX_passaway.transform.parent = null;
        VFX_passaway.Play();
        Destroy(gameObject);

    }

}


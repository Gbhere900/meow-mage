using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public float timeBeforeFirstJump = 3f;
    public float checkInterval = 1f;
    public float distance = 3;
    public float force = 10;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        InvokeRepeating("CheckDistance", timeBeforeFirstJump, checkInterval);
    }

    void CheckDistance()
    {
        if (player != null)
        {
            float tempDistance = Vector3.Distance(transform.position, player.transform.position);
            if (tempDistance < distance)
            {
                Jump();
            }
        }
    }

    void Jump()
    {

        Debug.LogError("jump");
        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce(direction * force);
    }
}

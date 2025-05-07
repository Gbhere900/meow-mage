using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    Rigidbody rb;
    public Vector3 swayDirection;
    [Header("ÊýÖµ")]
    public float swayForce = 100;
    public float timeBeforeFirstSway = 1;
    public float swayInterval;
    int flag = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnEnable()
    {
        InvokeRepeating("Sway", timeBeforeFirstSway, swayInterval);
    }

    public void Sway()
    {

        swayDirection = new Vector3(-rb.velocity.z * flag, 0f, rb.velocity.x * flag).normalized;
        //swayDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        flag *= -1;
        rb.AddForce(swayDirection * swayForce);
    }

}

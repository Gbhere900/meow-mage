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
    GameObject player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }
    private void OnEnable()
    {
        InvokeRepeating("Sway", timeBeforeFirstSway, swayInterval);
    }

    public void Sway()
    {
        Vector3 aimDirection = player.transform.position - transform.position;
        swayDirection = new Vector3(-aimDirection.z * flag, 0f, aimDirection.x * flag).normalized + aimDirection.normalized;
        //swayDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        flag *= -1;
        rb.AddForce(swayDirection * swayForce);
    }

}

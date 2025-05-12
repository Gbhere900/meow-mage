using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMove : MonoBehaviour
{
    public  Player player;
    public NavMeshAgent navMeshAgent;
    //private Rigidbody rigidbody;
    //private Vector3 targetDirection;

    //public float originalSpeed = 2;
    //public float speed = 2;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null && !navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent不在导航网格上，尝试重新启用", gameObject);

            //rigidbody = GetComponent<Rigidbody>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        //targetDirection = (player.transform.position - transform.position).normalized;
    }
    private void FixedUpdate()
    {
        //rigidbody.velocity = targetDirection * speed;
    }

    public void ChangeSpeedForSeconds(float speed,float seconds)
    {
        //this.speed = speed;
        StartCoroutine(WaitForSeconds(seconds, navMeshAgent.speed));
        navMeshAgent.speed = speed;
        
        
    }

    IEnumerator WaitForSeconds(float seconds,float speed)
    {
        yield return new WaitForSeconds(seconds);
        navMeshAgent.speed = speed;
        //this.speed = originalSpeed;
    }

}


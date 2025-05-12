using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bocchi : MonoBehaviour
{
    public GameObject player;
    public float timeBeforeFirstShoot = 3f;
    public float checkInterval = 1f;
    public float distance = 15;

    public float shootOffset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        InvokeRepeating("CheckDistance", timeBeforeFirstShoot, checkInterval);
    }

    void CheckDistance()
    {
        if (player != null)
        {
            float tempDistance = Vector3.Distance(transform.position, player.transform.position);
            if (tempDistance < distance)
            {
                Shoot();
                
            }
        }
    }

    void Shoot()
    {
        SlimeGelBullet slimeGelBullet =  EnemyBulletPoolManager.Instance().SpawnSlimeGelBullet(transform.position);
        
        //处理direction
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        transform.forward = direction;
        //
        Vector3 offsetVector = new Vector3(-direction.z, 0, direction.x).normalized;
        offsetVector *= UnityEngine.Random.Range((float)Math.Tan(-shootOffset * Mathf.Deg2Rad), (float)Math.Tan(shootOffset * Mathf.Deg2Rad));
        direction = direction + offsetVector;

        slimeGelBullet.transform.position = transform.position;
        slimeGelBullet.shootByDirection(direction);     //direction在函数里归一化了,这里不用归一化

        
    }
}

using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
 public class SlimeGelBullet : MonoBehaviour
{

    [Header("Bullet数值")]
    [SerializeField] private float speed = 5;

    [SerializeField] protected float damage = 3;

    public float time = 5;


   public  Rigidbody rigidbody;


    static public Action<SlimeGelBullet> OnRecycled;

    [Header("音效")]
    public AudioClip triggerAudio;


    protected void Awake()
    {
        triggerAudio = Resources.Load<AudioClip>("Audio/SoundEffect/ObstacleHit");
        if (triggerAudio == null)
        {
            Debug.Log("读取不到音频文件");
        }
        rigidbody = GetComponent<Rigidbody>();
    }
    protected void OnEnable()
    {
        StartCoroutine(WaitForDestroy());
    }

    protected void OnDisable()
    {
    }

    public void shootByDirection(Vector3 direction)
    {
        transform.forward = direction;
        rigidbody.velocity = direction.normalized * speed;
    }



    protected virtual void OnTriggerEnter(Collider other)            //super
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {

            other.GetComponent<PlayerHealth>().ReceiverDamage(damage);
            StopCoroutine(WaitForDestroy());
            Recycle();
            
        }
        if (other.gameObject.layer == 3)
        { 
                AudioManager.Instance().PlaySound(triggerAudio);
                StopCoroutine(WaitForDestroy());
                Recycle();
        }
    }
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(time);
        Recycle();
    }


    protected void Recycle()
    {
        OnRecycled.Invoke(this);
    }


}

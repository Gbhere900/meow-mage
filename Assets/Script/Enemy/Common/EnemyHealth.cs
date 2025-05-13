using System;

using UnityEngine;



public class EnemyHealth : MonoBehaviour
{
    [Header("数值")]
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health = 10;
    [SerializeField] private int EXPnum = 3;
    [SerializeField] private int Goldnum = 3;


    [Header("粒子效果")]
    public Transform TMPSpawnPoint;

    [Header("伤害文字效果")]
    [SerializeField] static public Action<float, Vector3> OnReceivedDamage;
    static public Action<Vector3> OnPassAway;
    static public Action<Vector3,int,int> OnGeneratingCollectable;

    [Header("音效")]
    public AudioClip hitAudio;
    public AudioClip passAwayAudio;
    private void Awake()
    {
        if ((hitAudio == null))
        {
            hitAudio = Resources.Load<AudioClip>("Audio/SoundEffect/EnemyHit");
        }
        if ((passAwayAudio == null))
        {
            passAwayAudio = Resources.Load<AudioClip>("Audio/SoundEffect/EnemyPassAway");
        }
       
    }
    private void OnEnable()
    {
        health = maxHealth;
    }

    public void ReceiveDamage(float damage)
    {
        OnReceivedDamage.Invoke(damage, TMPSpawnPoint.position);
        health -= Math.Min(health, damage);
        AudioManager.Instance().PlaySound(hitAudio);
        if (health == 0)
        {
            PassAway();
        }
    }
    public void PassAway()
    {
        AudioManager.Instance().PlaySound(passAwayAudio);
        OnPassAway?.Invoke(transform.position);
        OnGeneratingCollectable?.Invoke(transform.position,EXPnum,Goldnum);
        Destroy(gameObject);
    }
    public void PassAwayOnSwitchWave()
    {
        OnPassAway?.Invoke(transform.position);
        Destroy(gameObject);
    }
}

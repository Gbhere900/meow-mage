using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerResouces : MonoBehaviour
{
    static public PlayerResouces instance;
    [Header("经验")]
    [SerializeField] private Slider EXP_Slider;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private float maxEXP = 5 ;
    [SerializeField] private float currentEXP = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int lastLevel = 1;
    public  int deltaLevel = 1;

    [Header("金币")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private float currentGold = 0;


    [Header("收集半径")]
    [SerializeField]private SphereCollider sphereCollider;
    protected float colliderRadius = 2;

    [Header("音效")]
    AudioClip levelUPAudio;
    
    private void Awake()
    {
        levelUPAudio = Resources.Load<AudioClip>("Audio/SoundEffect/LevelUP");
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        instance = this;
    }
    private void OnEnable()
    {
        lastLevel = level;
        sphereCollider.radius = colliderRadius;
        UpdateLevelUI();

        EXPBall.OnCollected  += IncreaceEXP;
        GoldBall.OnCollected += IncreaceGold;
        WaveManager.OnWaveSwitched += UpdateDeltaLevel;
    }

    

    private void OnDisable()
    {
        EXPBall.OnCollected  -= IncreaceEXP;
        GoldBall.OnCollected -= IncreaceGold;
        WaveManager.OnWaveSwitched -= UpdateDeltaLevel;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>() != null)
        {
            Debug.Log("碰撞到Colllectable");
            Collectable tempCollectable=other.gameObject.GetComponent<Collectable>();
            tempCollectable.Collect();

        }
    }

    private void IncreaceEXP()
    {
        currentEXP++;
        if(currentEXP >= maxEXP)
        {
            LevelUP();
        }
        UpdateLevelUI();
    }

    public void  UpdateDeltaLevel()
    {
        deltaLevel = level - lastLevel;
        lastLevel = level;
    }

    public int GetDeltaLevel()
    {
        return deltaLevel;
    }

    private void LevelUP()
    {
        level++;
        currentEXP = 0;
        maxEXP = level * 5;

        AudioManager.Instance().PlaySound(levelUPAudio);
    }

    void UpdateLevelUI()
    {
        EXP_Slider.value = currentEXP/maxEXP;
        LevelText.text = level.ToString();
    }

    private void IncreaceGold()
    {
        currentGold++;

        UpdateGoldUI();
    }
    void UpdateGoldUI()
    {
        goldText.text = currentGold.ToString();
    }
}

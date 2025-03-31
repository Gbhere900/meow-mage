using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float generateDistance_min= 10;
    [SerializeField] private float generateDistance_max= 20;

    [Header("time")]
    [SerializeField] private float waveDuration = 30;
    [SerializeField] private float timer = 0;
    [SerializeField] private Boolean isTimerCounting = false;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Waves")] 
    [SerializeField] private Wave[] waves;
    [SerializeField] private int currentWave = -1;
    [SerializeField] private List<int> count;
    [SerializeField] private TextMeshProUGUI wavesText;
    private void Awake()
    {
        SwitchToNextWave();
        UpdateWavesText();
    }
    private void Update()
    {
        if (!isTimerCounting)
            return;

        UpdateTimerText();

        if (timer < waveDuration)
        {
            
            ManagerCurrentWave();
            timer += Time.deltaTime;
            
        }
            
        else
        {
            if(currentWave < waves.Length-1)
            {
                GameManager.instance.OnSwitchWaveCallBack(1);
                SwitchToNextWave();
            }
            
            else
            {
                isTimerCounting = false;
            }
          
        }
    }

    void SwitchToNextWave()
    {
        DestroyCurrentWaveEnemies();
        count.Clear();
        timer = 0;
        currentWave++;
        UpdateWavesText(); 
        for (int i = 0; i < waves[currentWave].segements.Count; i++)
            count.Add(1);
        
    }
    void ManagerCurrentWave()
    {
        Wave tempWave = waves[currentWave];

        for (int i = 0; i < tempWave.segements.Count; i++)
        {
            WaveSegement tempSegement = tempWave.segements[i];
            if(timer >= tempSegement.StartEndTime.x /100 * waveDuration &&timer <= tempSegement.StartEndTime.y/100 *waveDuration)
            {
                float timerFromStart = timer - tempSegement.StartEndTime.x;

                if(timerFromStart / tempSegement.Duration >= count[i])
                {
                    GameObject.Instantiate<GameObject>(tempSegement.prefab,GetGeneratePosition(),tempSegement.prefab.transform.rotation,this.transform);
                    count[i]++;

                }
            }
            
        }
       

    }

    private Vector3 GetGeneratePosition()
    {
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        Vector3 offset = direction * UnityEngine.Random.Range(generateDistance_min,generateDistance_max);
        Vector3 generatePosition= player.transform.position + offset;
        generatePosition.x = Math.Clamp(generatePosition.x, -24,24);
        generatePosition.z = Math.Clamp(generatePosition.z, -11f,11f);
        return generatePosition;
    }

    public  void DestroyCurrentWaveEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyHealth>()?.PassAwayOnSwitchWave();
        }
    }
    private void UpdateTimerText()
    {
        timerText.text = ((int)(timer)).ToString()+"  S";
    }
    private void UpdateWavesText()
    {
        wavesText.text = "Wave  " + (currentWave+1).ToString();
    }
    [System.Serializable]
    public struct Wave
    {
        public string name;
        public List<WaveSegement> segements;
    }

    [System.Serializable]
    public struct WaveSegement
    {
        [MinMaxSlider(0, 100)] public Vector2 StartEndTime;
        public float Duration;
        public GameObject prefab;
    }


}

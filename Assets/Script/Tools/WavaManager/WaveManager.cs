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
    public Transform generateBorder;
    [SerializeField] private float generateDistance_min= 30;
    [SerializeField] private float generateDistance_max= 50;

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
    public static Action OnWaveSwitched;
    private void Awake()
    {
        SwitchToNextWave();
        UpdateWavesText();
    }

    private void OnEnable()
    {
        GameManager.OnSwitchGameState += SwitchWaveState;
    }
    private void OnDisable()
    {
        GameManager.OnSwitchGameState -= SwitchWaveState;
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
                SwitchToNextWave();
                isTimerCounting = false ;
                //GameManager.instance.OnSwitchWaveCallBack();
                if(PlayerResouces.instance.deltaLevel >0)
                GameManager.instance.SwitchGameState(GameState.trophy);
                else
                {
                    GameManager.instance.SwitchGameState(GameState.wavetransition);
                }
            }
            
            else
            {
                isTimerCounting = false;
                DestroyCurrentWaveEnemies();
                GameManager.instance.SwitchGameState(GameState.victory);
            }
          
        }
    }

    void SwitchToNextWave()
    {
        DestroyCurrentWaveEnemies();
        timer = 0;
        currentWave++;
        UpdateCount();
        UpdateWavesText();
        OnWaveSwitched?.Invoke();
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
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        Vector3 offset = direction * UnityEngine.Random.Range(generateDistance_min,generateDistance_max);
        Vector3 generatePosition= player.transform.position + offset;
        generatePosition.x = Math.Clamp(generatePosition.x, -generateBorder.position.x,generateBorder.position.x);
        generatePosition.z = Math.Clamp(generatePosition.z, -generateBorder.position.y, generateBorder.position.y);
        return generatePosition;
    }

    public  void DestroyCurrentWaveEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<EnemyHealth>()?.PassAwayOnSwitchWave();
        }
    }

    private void UpdateCount()
    {
        count.Clear();
        for (int i = 0; i < waves[currentWave].segements.Count; i++)
            count.Add(1);
    }

    private void UpdateTimerText()
    {
        timerText.text = ((int)(timer)).ToString()+"  S";
    }
    private void UpdateWavesText()
    {
        wavesText.text = "µÚ" + (currentWave+1).ToString()+"²¨";
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

    private void SwitchWaveState(GameState state)
    {
        if(state == GameState.game)
        {
            isTimerCounting = true;
        }
        else
        {
            isTimerCounting = false;
        }
    }

}

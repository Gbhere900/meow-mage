using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float waveDuration = 30;
    [SerializeField] private float timer = 0;

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private int count = 0;
    private void Update()
    {
        if (timer < waveDuration)
        {
            ManagerCurrentWave();
            timer += Time.deltaTime;
        }
            
        else
        {
            timer = 0;
        }
    }

    void ManagerCurrentWave()
    {
        Wave tempWave = waves[0];

        for (int i = 0; i < tempWave.segements.Count; i++)
        {
            WaveSegement tempSegement = tempWave.segements[i];
            if(timer >= tempSegement.StartEndTime.x /100 * waveDuration &&timer <= tempSegement.StartEndTime.y/100 *waveDuration)
            {
                float timerFromStart = timer - tempSegement.StartEndTime.x;

                if(timerFromStart / tempSegement.Duration >= count)
                {
                    GameObject.Instantiate<GameObject>(tempSegement.prefab);
                    count++;

                }
            }
        }
        
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

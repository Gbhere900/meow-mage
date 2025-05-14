using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class TrophyManager : MonoBehaviour
{
    static private TrophyManager instance;
    [SerializeField] private MagicSO[] magicSOs;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    public TrophyButton TrophyButtonPrefabs;
    [SerializeField] private MagicBase[] magics;

    
    static public TrophyManager Instance()
    {
        return instance;
    }
    private void Start()
    {
        ChangeTrophies();
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        GameManager.OnSwitchGameState += ChangeTroPhiesOnSwitchGameState;
        
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    [NaughtyAttributes.Button]
    public void ChangeTrophies()
    {
       ShuffleMagicSOs();
      // ShuffleMagics();
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
            GameObject tempTrophyButton = horizontalLayoutGroup.transform.GetChild(i).gameObject;
            Destroy(tempTrophyButton);
        }
        for(int i=0;i<3;i++)
        {
            TrophyButton trophyButton = Instantiate(TrophyButtonPrefabs, horizontalLayoutGroup.transform);

            trophyButton.GetButton().onClick.RemoveAllListeners();
            trophyButton.ChangeTrophyByMagicSO(magicSOs[i]);
            trophyButton.GetButton().onClick.AddListener(() => ChangeVisualEffect(trophyButton));
        }

    }

    private void ChangeVisualEffect(TrophyButton tempTrophyButton)
    {
        tempTrophyButton.Selected();
        for (int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
        {
           
            if ( horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>() != tempTrophyButton)
            {
                horizontalLayoutGroup.transform.GetChild(i).GetComponent<TrophyButton>().DisSelected();
            }
        }

    }

    private void ChangeTroPhiesOnSwitchGameState(GameState gameState)
    {
        if(gameState ==GameState.game)
        {
            ChangeTrophies();
        }
    }


   public void ShuffleMagicSOs()
   {
      // Fisher-Yates洗牌算法
      for (int i = magicSOs.Length - 1; i > 0; i--)
      {
          int j = UnityEngine.Random.Range(0, i + 1);
          (magicSOs[i], magicSOs[j]) = (magicSOs[j], magicSOs[i]);
       }
    // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
   }
    public void ShuffleMagics()
    {
        // Fisher-Yates洗牌算法
        for (int i = magics.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (magics[i], magics[j]) = (magics[j], magics[i]);
        }
        // return allBuffs.GetRange(0, Mathf.Min(count, allBuffs.Count));
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // 确保场景销毁时清空引用
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        instance = null;
    }
}

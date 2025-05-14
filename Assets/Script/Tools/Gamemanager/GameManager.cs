using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public Action<GameState> OnSwitchGameState;
    static public Action OnPaused;
    static public Action OnContinued;
    public GameState gameState;
    public bool isPaused = false;
    PlayerInputControl playerInputControl;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
            instance = this;

        SwitchToMenu();
        playerInputControl = new PlayerInputControl();
        
    }
    private void OnEnable()
    {
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        playerInputControl.Enable();
        playerInputControl.Player.ESC.started += OnESCClicked;
    }

    private void OnDisable()
    {
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        playerInputControl.Disable();
        playerInputControl.Player.ESC.started -= OnESCClicked;
    }

    private void OnESCClicked(InputAction.CallbackContext context)
    {
        //Debug.LogError("ESC");
        if (UIManager.Instancce().gameUI.activeInHierarchy)
        {
            SwitchPauseState();
        }
    }

    public void SwitchPauseState()
    {
        if (isPaused)
        {
            Continue();
        }
        else
        {
            Pause();
        }
    }

    static public GameManager Instance()
    {
        return instance;
    }


    public void Pause()
    { 
        isPaused = true;
        Time.timeScale = 0.0f;
        OnPaused.Invoke();
    }
    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        OnContinued.Invoke();
    }
    public void SwitchGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnSwitchGameState.Invoke(gameState);
        Debug.Log(gameState.ToString());
    }

    public void SwitchToMenu()
    {
        SwitchGameState(GameState.menu);
    }

    public void SwitchToGame()
    {
        SwitchGameState(GameState.game);
    }
    public void SwitchToWaveTransition()
    {
        SwitchGameState(GameState.wavetransition);
    }
    public void SwitchToBag()
    {
        SwitchGameState(GameState.bag);
    }
    public void SwitchToThophy()
    {
        SwitchGameState(GameState.trophy);
    }
    public void SwitchToShop()
    {
        SwitchGameState(GameState.shop);
    }
    public void SwitchToGameOver()
    {
        SwitchGameState(GameState.gameOver);
    }
    public void SwitchToVictory()
    {
        SwitchGameState(GameState.victory);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // 确保场景销毁时清空引用
        }
    }


}

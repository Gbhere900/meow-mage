using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject root;
    public Button[] ButtonDontPause;
    protected Button[] ButtonToPause;
    // Start is called before the first frame update
    private void Awake()
    {
        ButtonToPause = root.gameObject.GetComponentsInChildren<Button>();
        
    }
    private void OnEnable()
    {
        SetAllButtonDisinteractive();
        foreach (var button in ButtonDontPause)
        {
            button.interactable = true;
        }
    }
    private void OnDisable()
    {
        SetAllButtoninteractive();
    }

    public void SetAllButtonDisinteractive()
    {
        foreach (var item in ButtonToPause)
        {
            item.interactable = false;
        }
    }

    public void SetAllButtoninteractive()
    {
        foreach (var item in ButtonToPause)
        {
            item.interactable = true;
        }
    }
}

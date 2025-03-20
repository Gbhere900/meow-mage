using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    private PlayerInputControl playerInputControl;
    public Camera mainCamera;
    public void Awake()
    {
        playerInputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        playerInputControl.Enable();
    }
    // Update is called once per frame
    void Update()
    {
       Debug.Log(GetMousePosition());
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
    }
     public Vector2 GetMousePosition()
    {
        return playerInputControl.Player.MousePosition.ReadValue<Vector2>();
    }
}

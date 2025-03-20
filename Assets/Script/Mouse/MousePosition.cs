using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

static public class MousePosition 
{
    static private PlayerInputControl playerInputControl;
    static public Camera mainCamera;
    static MousePosition()
        {
            playerInputControl = new PlayerInputControl();
            playerInputControl.Enable();
            mainCamera = Camera.main;
        }




    static public Vector3 GetMousePosition()
    {
        Vector3 position = playerInputControl.Player.MousePosition.ReadValue<Vector2>();
        position.z = 29.5f;
        return Camera.main.ScreenToWorldPoint(position);
    }
}

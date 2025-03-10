using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputControl playerInputControl;
    private Rigidbody rigidbody;

    [SerializeField] Vector2 inputDirection;
    [SerializeField] float yVelocity;
    void Awake()
    {
        playerInputControl = new PlayerInputControl();  
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.right;
    }

    private void OnEnable()
    {
        playerInputControl.Enable();
    }
    void Update()
    {
        inputDirection = playerInputControl.Player.Move.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
    }
}

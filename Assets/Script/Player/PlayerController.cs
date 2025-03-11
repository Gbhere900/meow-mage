using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    
    private PlayerInputControl playerInputControl;
    private Rigidbody rigidbody;

    public float speed = 3;

    [SerializeField] Vector3 inputDirection;
    
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
    private void FixedUpdate()
    {
        inputDirection.x = playerInputControl.Player.Move.ReadValue<Vector2>().x;
        inputDirection.z = playerInputControl.Player.Move.ReadValue<Vector2>().y;
        rigidbody.velocity = inputDirection * speed;
    }
    
    private void OnDisable()
    {
        playerInputControl.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    
    private PlayerInputControl playerInputControl;
    private Rigidbody rigidbody;

    public float BasicSpeed = 4;
    public float speed = 4;
    public float timeToRecover = 2f;
    public bool canControl = true;
    public GameObject exclamationMark;
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
        Move();
        SetRotation();
    }

    private void Move()
    {
        if (canControl)
        {
            inputDirection.x = playerInputControl.Player.Move.ReadValue<Vector2>().x;
            inputDirection.z = playerInputControl.Player.Move.ReadValue<Vector2>().y;
            rigidbody.velocity = inputDirection * speed;
        }
    }
    private void SetRotation()
    {
        Vector3 forward = MousePosition.GetMousePosition();
        forward.y = transform.position.y;
        transform.forward = forward - transform.position;
    }
    
    private void OnDisable()
    {
        playerInputControl.Disable();
    }

    public void BePushed(Vector3 position, float force)
    {
        
        StopCoroutine(WaitForRecover(timeToRecover));
        StartCoroutine(WaitForRecover(timeToRecover));
        Vector3 direction = (transform.position - position);
        direction.y = 0;
        direction = direction.normalized;
        rigidbody.AddForce(direction * force, ForceMode.Force);
    }
    IEnumerator WaitForRecover(float time)
    {
        // speed = BasicSpeed / 2;
        //playerInputControl.Disable();
        canControl = false;
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(time);
        exclamationMark.SetActive(false);
        canControl = true;
        //if (GameManager.Instance().gameState == GameState.game)
           // playerInputControl.Enable();
        //speed = BasicSpeed;
    }

    public void AddSpeed()
    {
        speed += 1f;
    }
}

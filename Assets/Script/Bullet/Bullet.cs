using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Bullet : MonoBehaviour
{
    [Header("ÊýÖµ")]
    [SerializeField] float speed = 5;
    [SerializeField] float damage = 3;
    //[SerializeField] private MousePosition mousePositionManager;

    private Rigidbody rigidbody;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();  
    }
    private void OnEnable()
    {
        rigidbody.velocity = (MousePosition.GetMousePosition()- transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

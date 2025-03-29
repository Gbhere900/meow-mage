using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResouces : MonoBehaviour
{
    [Header("ÊÕ¼¯°ë¾¶")]
    [SerializeField]private SphereCollider sphereCollider;
    protected float colliderRadius = 2;
    
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        sphereCollider.radius = colliderRadius;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>() != null)
        {
            Debug.Log("Åö×²µ½Colllectable");
            Collectable tempCollectable=other.gameObject.GetComponent<Collectable>();
            tempCollectable.Collect();
        }
    }

}

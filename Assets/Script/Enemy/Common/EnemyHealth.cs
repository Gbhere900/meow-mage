using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem VFX_passaway;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassAway()
    {
        VFX_passaway.transform.parent = null;
        VFX_passaway.Play();
        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    MousePosition mousePosition;
    // Start is called before the first frame update
    private void Awake()
    {
        mousePosition = GetComponent<MousePosition>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Vector3 position = mousePosition.GetMousePosition();
        transform.position = mousePosition.mainCamera.ScreenToWorldPoint(position);
        Debug.Log(transform.position);
    }
}

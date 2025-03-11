using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform transfromToFollow;
    private Vector3 cameraTransfrom;
    private void LateUpdate()
    {
        cameraTransfrom = transfromToFollow.transform.position;
        cameraTransfrom.y = 30;
        this.transform.position = cameraTransfrom;
    }
    void Update()
    {
        
    }
}

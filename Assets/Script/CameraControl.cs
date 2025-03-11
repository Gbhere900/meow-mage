using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform transfromToFollow;

    private Vector3 cameraTransfrom;

    public Transform constraint;
    private void LateUpdate()
    {
        
        cameraTransfrom.x = Math.Clamp(transfromToFollow.position.x, -constraint.position.x, constraint.position.x);
        cameraTransfrom.z = Math.Clamp(transfromToFollow.position.z, -constraint.position.z, constraint.position.z);
        cameraTransfrom.y = 30;
        this.transform.position = cameraTransfrom;
    }
    void Update()
    {
        
    }
}

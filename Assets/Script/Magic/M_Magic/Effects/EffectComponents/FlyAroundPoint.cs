using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAroundPoint : MonoBehaviour
{
    public Vector3 centerPoint;
    public float rotationSpeed = 90f;
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (centerPoint == Vector3.zero)
        {
            Debug.LogError("中心点为空");
        }
        
        Vector3 toCenter = centerPoint - transform.position;
        // 计算目标速度方向（垂直于圆心连线）
        Vector3 targetVelocity = Vector3.Cross(toCenter.normalized, Vector3.up) * rb.velocity.magnitude;

        // 调整实际速度方向逼近目标方向
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f);


        //float deltaAngle = rotationSpeed * Time.fixedDeltaTime;
        //// 绕 Y 轴（垂直轴）旋转方向
        //Quaternion rot = Quaternion.Euler(0, deltaAngle, 0);
        //// 旋转速度方向并保持速率不变
        //Vector3 newDirection = rot * rb.velocity.normalized;
        //rb.transform.forward = newDirection;
        //rb.velocity = newDirection * rb.velocity.magnitude;

    }
    private void OnDisable()
    {
        // 仅在物体被禁用时销毁组件（而非组件自身被禁用）
        if (!gameObject.activeSelf)
        {
            Destroy(this); // 销毁自身组件
        }
    }
}

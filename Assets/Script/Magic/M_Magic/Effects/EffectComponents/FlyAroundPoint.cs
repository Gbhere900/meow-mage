using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAroundPoint : MonoBehaviour
{
    public GameObject centerObejct;
    public Vector3 centerPoint;
    public float timeBeforeActive = 0.3f;
    public float timer = 0;
    public float rotationSpeed = 90f;
    public Rigidbody rb;
    public Bullet bullet;
    public bool isActive = false;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        bullet = GetComponent<Bullet>();
        
        
    }


      
        private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=timeBeforeActive)
        {
            isActive = true;
        }
    }


    private void FixedUpdate()
    {
        if (!isActive)
            return;
        if(!centerObejct.active)
        {
            isActive = false;
        }
        centerPoint = centerObejct.transform.position;
        if (centerPoint == Vector3.zero)
        {
            Debug.LogError("���ĵ�Ϊ��");
        }
        
        Vector3 toCenter = centerPoint - transform.position;
        // ����Ŀ���ٶȷ��򣨴�ֱ��Բ�����ߣ�
        Vector3 targetVelocity = Vector3.Cross(toCenter.normalized, Vector3.up) * bullet.Speed;

        // ����ʵ���ٶȷ���ƽ�Ŀ�귽��
        rb.velocity = targetVelocity;
        transform.forward = targetVelocity;


        //float deltaAngle = rotationSpeed * Time.fixedDeltaTime;
        //// �� Y �ᣨ��ֱ�ᣩ��ת����
        //Quaternion rot = Quaternion.Euler(0, deltaAngle, 0);
        //// ��ת�ٶȷ��򲢱������ʲ���
        //Vector3 newDirection = rot * rb.velocity.normalized;
        //rb.transform.forward = newDirection;
        //rb.velocity = newDirection * rb.velocity.magnitude;

    }
    private void OnDisable()
    {
        // �������屻����ʱ�����������������������ã�
        if (!gameObject.activeSelf)
        {
            Destroy(this); // �����������
        }
    }
}

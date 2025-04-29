using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAroundPoint : MonoBehaviour
{
    public GameObject centerObejct;
    public Vector3 centerPoint;
    public float timeBeforeActive = 0.3f;
    public float rotationSpeed = 90f;
    public float speed;
    public Rigidbody rb;
    public bool isActive = false;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeBeforeActive);
        speed = rb.velocity.magnitude;
        isActive = true;
    }


    private void FixedUpdate()
    {
        if (!isActive)
            return;
        centerPoint = centerObejct.transform.position;
        if (centerPoint == Vector3.zero)
        {
            Debug.LogError("���ĵ�Ϊ��");
        }
        
        Vector3 toCenter = centerPoint - transform.position;
        // ����Ŀ���ٶȷ��򣨴�ֱ��Բ�����ߣ�
        Vector3 targetVelocity = Vector3.Cross(toCenter.normalized, Vector3.up) * speed;

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

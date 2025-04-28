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
            Debug.LogError("���ĵ�Ϊ��");
        }
        
        Vector3 toCenter = centerPoint - transform.position;
        // ����Ŀ���ٶȷ��򣨴�ֱ��Բ�����ߣ�
        Vector3 targetVelocity = Vector3.Cross(toCenter.normalized, Vector3.up) * rb.velocity.magnitude;

        // ����ʵ���ٶȷ���ƽ�Ŀ�귽��
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f);


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

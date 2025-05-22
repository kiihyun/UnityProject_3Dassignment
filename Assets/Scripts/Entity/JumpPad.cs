using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float power = 5f;

    void OnTriggerEnter(Collider other)
    {
        m_Rigidbody = other.GetComponent<Rigidbody>();
        // �Է��� ��(����)���� �ؼ��ϰ�, �ӵ��� '�� * DT / ����'�� ������ ����.        // ȿ���� �ùķ��̼� �ܰ� ���̿� ��ü�� ������ ���� �޶����ϴ�.
        //m_Rigidbody.AddForce(transform.up * power);

        // �Ű������� ��ݷ�(����-��)���� �ؼ��ϰ�, ��/���� ������ �ӵ��� ����.
        // ȿ���� ��ü�� ������ ���� �޶�������, �ùķ��̼� �ܰ� ���̿��� ���� X
        m_Rigidbody.AddForce(transform.up * power, ForceMode.Impulse);
    }
}

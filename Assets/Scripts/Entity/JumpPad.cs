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
        // 입력을 힘(뉴턴)으로 해석하고, 속도를 '힘 * DT / 질량'의 값으로 변경.        // 효과는 시뮬레이션 단계 길이와 물체의 질량에 따라 달라집니다.
        //m_Rigidbody.AddForce(transform.up * power);

        // 매개변수를 충격량(뉴턴-초)으로 해석하고, 힘/질량 값으로 속도를 변경.
        // 효과는 물체의 질량에 따라 달라지지만, 시뮬레이션 단계 길이에는 영향 X
        m_Rigidbody.AddForce(transform.up * power, ForceMode.Impulse);
    }
}

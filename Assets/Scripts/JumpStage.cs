using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStage : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
            if (_rigidbody != null)
            {
                // 기존 속도 제거하고 위로 점프
                Vector3 velocity = _rigidbody.velocity;
                velocity.y = 0;
                _rigidbody.velocity = velocity;

                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirHitBullet : MonoBehaviour
{
    [SerializeField] private Transform footCenter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Vector3 hitDir = footCenter.InverseTransformDirection(footCenter.position - contactPoint.point).normalized;

            if (hitDir.x > 0)
                hitDir.x = 1;
            else
                hitDir.x = -1;

            if (hitDir.z > 0)
                hitDir.z = 1;
            else
                hitDir.z = -1;

            animator.SetFloat("HitX", hitDir.x);
            animator.SetFloat("HitZ", hitDir.z);
        }
    }
}

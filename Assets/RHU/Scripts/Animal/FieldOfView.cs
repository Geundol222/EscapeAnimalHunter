using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("FieldOfView")]
    [SerializeField] public float trackingRange;
    [SerializeField] public float attackRange;
    [SerializeField, Range(0, 360)] public float angle;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstacleMask;
    [NonSerialized] public bool foundPlayer;
    [NonSerialized] public Transform playerTransform;

    //[Header("FieldOfView")]
    //public float trackingRange;
    //public float angle;
    //public LayerMask targetMask;
    //public LayerMask obstacleMask;
    //public bool foundPlayer;

    //private void Awake()
    //{
    //    trackingRange = 150;
    //    angle = 90;
    //    targetMask = LayerMask.NameToLayer("Player");
    //    obstacleMask = LayerMask.NameToLayer("Object");
    //    foundPlayer = false;
    //}

    public bool FindPlayer()
    {
        Collider[] trackingColliders = Physics.OverlapSphere(transform.position, trackingRange, targetMask);            // 1. ���� �ȿ� �ִ���
        Collider[] attackColliders = Physics.OverlapSphere(transform.position, trackingRange, targetMask);

        foreach (Collider collider in trackingColliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;          // 2. �տ� �ִ���
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))    // Dot�� ����
                continue;                                                               // ȣ����

            float distToTarget = Vector3.Distance(transform.position, collider.transform.position);
            
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))             // 3. �߰��� ��ֹ��� ������
                continue;

            Debug.Log(foundPlayer);
            Debug.DrawRay(transform.position, dirTarget * distToTarget, Color.red);
            playerTransform = collider.transform;

            return foundPlayer = true;
        }

        return foundPlayer = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, trackingRange);
        Vector3 rightDir1 = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir1 = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir1 * trackingRange, Color.red);
        Debug.DrawRay(transform.position, leftDir1 * trackingRange, Color.red);

        Gizmos.DrawWireSphere(transform.position, attackRange);
        Vector3 rightDir2 = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir2 = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir2 * attackRange, Color.blue);
        Debug.DrawRay(transform.position, leftDir2 * attackRange, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}

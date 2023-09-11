using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] public float trackingRange;
    [SerializeField] public float attackRange;
    [SerializeField, Range(0, 360)] public float trackingAngle;
    [SerializeField, Range(0, 360)] public float attackAngle;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstacleMask;
    [NonSerialized] public bool foundPlayer;
    [NonSerialized] public Transform playerTransform;

    private bool FindPlayer(float range, float angle)
    {
        Collider[] trackingColliders = Physics.OverlapSphere(transform.position, range, targetMask);    // 1. 범위 안에 있는지

        foreach (Collider collider in trackingColliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;          // 2. 앞에 있는지
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))    // Dot은 내적
                continue;                                                               // 호도법

            float distToTarget = Vector3.Distance(transform.position, collider.transform.position);
            
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))             // 3. 중간에 장애물이 없는지
                continue;

            Debug.DrawRay(transform.position, dirTarget * distToTarget, Color.red);
            playerTransform = collider.transform;

            return foundPlayer = true;
        }

        return foundPlayer = false;
    }

    public bool TrackingFOV()
    {
        return FindPlayer(trackingRange, trackingAngle);
    }

    public bool AttackFOV()
    {
        return FindPlayer(attackRange, attackAngle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, trackingRange);                           // tracking
        Vector3 rightDir1 = AngleToDir(transform.eulerAngles.y + trackingAngle * 0.5f);
        Vector3 leftDir1 = AngleToDir(transform.eulerAngles.y - trackingAngle * 0.5f);
        Debug.DrawRay(transform.position, rightDir1 * trackingRange, Color.red);
        Debug.DrawRay(transform.position, leftDir1 * trackingRange, Color.red);

        Gizmos.DrawWireSphere(transform.position, attackRange);                             // attack
        Vector3 rightDir2 = AngleToDir(transform.eulerAngles.y + attackAngle * 0.5f);
        Vector3 leftDir2 = AngleToDir(transform.eulerAngles.y - attackAngle * 0.5f);
        Debug.DrawRay(transform.position, rightDir2 * attackRange, Color.blue);
        Debug.DrawRay(transform.position, leftDir2 * attackRange, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}

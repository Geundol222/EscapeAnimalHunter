using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("FieldOfView")]
    [SerializeField] public float range;
    [SerializeField, Range(0, 360)] public float angle;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstacleMask;
    [NonSerialized] public bool foundPlayer;
    [NonSerialized] public Transform playerTransform;

    //[Header("FieldOfView")]
    //public float range;
    //public float angle;
    //public LayerMask targetMask;
    //public LayerMask obstacleMask;
    //public bool foundPlayer;

    //private void Awake()
    //{
    //    range = 150;
    //    angle = 90;
    //    targetMask = LayerMask.NameToLayer("Player");
    //    obstacleMask = LayerMask.NameToLayer("Object");
    //    foundPlayer = false;
    //}

    public bool FindPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);            // 1. ���� �ȿ� �ִ���

        foreach (Collider collider in colliders)
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
        Gizmos.DrawWireSphere(transform.position, range);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.red);
        Debug.DrawRay(transform.position, leftDir * range, Color.red);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}

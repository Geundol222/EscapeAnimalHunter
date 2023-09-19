using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private int maxDistance;
    [SerializeField] private LayerMask playerLayer;

    private SphereCollider col;

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
        col.radius = maxDistance;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer == playerLayer)
        //    GameManager.Spawn
    }
}

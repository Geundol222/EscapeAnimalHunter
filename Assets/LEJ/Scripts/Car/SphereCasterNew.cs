using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereCasterNew : MonoBehaviour
{
    [SerializeField] float radiusMax; //15f
    [SerializeField] float radiusGrowUpSpeed; //10f
    public UnityAction<Transform> OnDetect;

    float maxDistance = 0.1f;
    float curRadius;
    int layerMask;
    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Radar");
        curRadius = radiusMax;
    }

    private void Update()
    {
        curRadius += radiusGrowUpSpeed * Time.deltaTime;

        if (curRadius > radiusMax)
            curRadius = 0;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, curRadius, transform.forward, maxDistance, layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            OnDetect?.Invoke(hits[i].transform);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (Physics.SphereCast(transform.position, curRadius, transform.forward, out RaycastHit hit, maxDistance))
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, curRadius);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * maxDistance, curRadius);
        }
    }
}
